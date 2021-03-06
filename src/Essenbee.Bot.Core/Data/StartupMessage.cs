﻿namespace Essenbee.Bot.Core.Data
{
    public class StartupMessage : DataEntity
    {
        public StartupMessage()
        {
        }

        public StartupMessage(string message, ItemStatus status)
        {
            Message = message;
            Status = status;
        }

        public string Message { get; set; }
        public ItemStatus Status { get; set; }
    }
}
