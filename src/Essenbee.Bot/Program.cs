﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using Essenbee.Bot.Core.Utilities;
using Essenbee.Bot.Infra.Slack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Console;

namespace Essenbee.Bot
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        private static IList<IChatClient> _connectedChatClients;
        private static bool _endProgram = false;

        public static void Main(string[] args)
        {
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");


            if (isDevelopment) // Only add secrets in development
            {
                builder.AddUserSecrets<UserSecrets>();
            }

            Configuration = builder.Build();

            WriteLine("CoreBot is getting ready....");

            Console.CancelKeyPress += OnCtrlC;

            // Make UserSecrets injectable ...
            var services = new ServiceCollection()
                .Configure<UserSecrets>(Configuration.GetSection(nameof(UserSecrets)))
                .AddOptions()
                .AddSingleton<UserSecretsProvider>()
                .BuildServiceProvider();

            services.GetService<UserSecrets>();

            var secrets = services.GetService<UserSecretsProvider>().Secrets;
            var defaultChannel = "general";

            WriteLine("Press [Ctrl]+C to exit.");

            var autoMessaging = new AutoMessaging(new SystemClock());

            var testMsg = new TimerTriggeredMessage
            {
                Delay = 1, // Minutes
                Message = "Hi, this is a timed message from CoreBot!"
            };

            autoMessaging.PublishMessage(testMsg);

            // Handle multiple chat clients that implement IChatClient ...
            _connectedChatClients = ConnectChatClients(secrets);

            while (true)
            {
                Thread.Sleep(1000);

                // Show Timer Triggered Messages
                autoMessaging.EnqueueMessagesToDisplay();

                while (true)
                {
                    var result = autoMessaging.DequeueNextMessage();

                    if (!result.isMessage) break;

                    foreach (var client in _connectedChatClients)
                    {
                        var channelId = client.Channels.ContainsKey(defaultChannel) ? 
                            client.Channels[defaultChannel] 
                            : string.Empty;

                        client.PostMessage(channelId,
                            $"{DateTime.Now.ToShortTimeString()} - {result.message}");
                    }
                }

                if (_endProgram) break;
            }
        }

        private static List<IChatClient> ConnectChatClients(UserSecrets secrets)
        {
            var slackApiKey = secrets.SlackApiKey;

            var connectedClients = new List<IChatClient>
            {
                new ConsoleChatClient(),
                new SlackChatClient(slackApiKey),
            };

            Thread.Sleep(5000);

            return connectedClients;
        }

        private static void OnCtrlC(object sender, ConsoleCancelEventArgs e)
        {
            foreach (var client in _connectedChatClients)
            {
                client.Disconnect();
            }

            _endProgram = true;
        }
    }
}
