﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using Hangfire;
using Hangfire.Storage.Monitoring;

namespace Essenbee.Bot.Infra.Hangfire
{
    public class HangfireActionScheduler : IActionScheduler
    {
        public IList<IChatClient> ChatClients { get; }

        public HangfireActionScheduler(IConnectedClients clients)
        {
            ChatClients = clients.ChatClients;
        }

        public void Enqueue(Expression<Action> action)
        {
            BackgroundJob.Enqueue(action);
        }

        public void Schedule(IScheduledAction action)
        {
            if (ChatClients is null)
            {
                throw new InvalidDataException("Chat clients property is not set!"); ;
            }

            switch (action)
            {
               case DelayedMessage delayedMsg:
                    var msg = delayedMsg.Message;
                    var chnl = delayedMsg.Channel;

                    foreach (var chatClient in ChatClients)
                    {
                        if (!string.IsNullOrWhiteSpace(chnl))
                        {
                            BackgroundJob.Schedule(() => chatClient.PostMessage(chnl, msg), delayedMsg.Delay);
                        }
                        else
                        {
                            BackgroundJob.Schedule(() => chatClient.PostMessage(msg), delayedMsg.Delay);
                        }
                    }
                    break;

                case RepeatingMessage repeatingMsg:
                    var message = repeatingMsg.Message;
                    var channel = repeatingMsg.Channel;

                    foreach (var chatClient in ChatClients)
                    {
                        if (!string.IsNullOrWhiteSpace(channel))
                        {
                            RecurringJob.AddOrUpdate(
                            repeatingMsg.Name,
                            () => chatClient.PostMessage(channel, message),
                            Cron.MinuteInterval(repeatingMsg.IntervalInMinutes));
                        }
                        else
                        {
                            RecurringJob.AddOrUpdate(
                            repeatingMsg.Name,
                            () => chatClient.PostMessage(message),
                            Cron.MinuteInterval(repeatingMsg.IntervalInMinutes));
                        }
                    }
                    break;
            }
        }

        public List<string> GetRunningJobs<T>()
        {
            var jobs = GetRunningHangfireJobs().Where(o => o.Value.Job.Type == typeof(T));
            return jobs.Select(j => j.Key).ToList();
        }

        public void StopRunningJobs<T>()
        {
            var jobs = GetRunningJobs<T>();
            if (jobs.Any())
            {
                foreach (var job in jobs)
                {
                    BackgroundJob.Delete(job);
                    RecurringJob.RemoveIfExists(job);
                }
            }
        }

        public List<string> GetRunningJobs()
        {
            var jobs = GetRunningHangfireJobs();
            return jobs.Select(j => j.Key).ToList();
        }

        public List<string> GetScheduledJobs()
        {
            var jobs = GetScheduledHangfireJobs();
            return jobs.Select(j => j.Key).ToList();
        }

        public List<string> GetEnqueuedJobs()
        {
            var jobs = GetEnqueuedHangfireJobs();
            return jobs.Select(j => j.Key).ToList();
        }

        private List<KeyValuePair<string, ProcessingJobDto>> GetRunningHangfireJobs()
        {
            return JobStorage.Current.GetMonitoringApi()
                .ProcessingJobs(0, int.MaxValue).ToList();
        }

        private List<KeyValuePair<string, ScheduledJobDto>> GetScheduledHangfireJobs()
        {
            return JobStorage.Current.GetMonitoringApi()
                .ScheduledJobs(0, int.MaxValue).ToList();
        }

        private List<KeyValuePair<string, EnqueuedJobDto>> GetEnqueuedHangfireJobs(string queue = "default")
        {
            return JobStorage.Current.GetMonitoringApi()
                .EnqueuedJobs(queue, 0, int.MaxValue).ToList();
        }
    }
}
