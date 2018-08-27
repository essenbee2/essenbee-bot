﻿using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public static class ItemFactory
    {
        public static AdventureItem GetInstance(IReadonlyAdventureGame game, Item itemType)
        {
            switch(itemType)
            {
                case Item.Bottle:
                    return new Bottle(game, "bottle");
                case Item.ShardOfGlass:
                    return new ShardOfGlass(game, "glass", "shard");
                case Item.FoodRation:
                    return new FoodRation(game, "food", "ration");
                case Item.Grate:
                    return Grate.GetInstance(game, "grate"); // Singleton
                case Item.Key:
                    return new Key(game, "key");
                case Item.Lamp:
                    return new Lamp(game, "lamp");
                case Item.Leaflet:
                    return new Leaflet(game, "leaflet", "flyer");
                case Item.Mailbox:
                    return new Mailbox(game, "mailbox");
                case Item.PintOfWater:
                    return new PintOfWater(game, "water");
                case Item.BrokenGlass:
                    return new BrokenGlass(game, "glass");
                case Item.Cage:
                    return new Cage(game, "cage");
                case Item.Unknown:
                default:
                    break;
            }

            return null;
        }
    }
}
