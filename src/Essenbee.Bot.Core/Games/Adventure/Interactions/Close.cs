﻿using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class Close : IAction
    {
        public bool Do(AdventurePlayer player, AdventureItem item)
        {
            var location = player.CurrentLocation;

            if (item is null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You cannot see a {item.Name} here!");
                return false;
            }

            if (!item.IsOpen)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"The {item.Name} is already closed!");
                return false;
            }

            player.ChatClient.PostDirectMessage(player.Id, $"You have closed the {item.Name}.");
            item.IsOpen = false;
            return true;
        }
    }
}