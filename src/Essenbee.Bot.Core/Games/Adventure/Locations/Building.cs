﻿using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Building : AdventureLocation
    {
        public Building(IReadonlyAdventureGame game) : base(game)
        {
            var bottle = ItemFactory.GetInstance(Game, "bottle");
            var lamp = ItemFactory.GetInstance(Game, "lamp");
            var key = ItemFactory.GetInstance(Game, "key");
            var food = ItemFactory.GetInstance(Game, "food");

            LocationId = "building";
            Name = "Small Brick Building";
            ShortDescription = "inside a small brick building.";
            LongDescription = " inside a small brick building, a well house for a bubbling spring.";
            WaterPresent = true;
            Items = new List<AdventureItem>
            {
                key,
                lamp,
                bottle,
                food,
            };
            Moves = new Dictionary<string, string> {
                        {"west", "road" },
                        {"w", "road" },
                        {"road", "road" },
                        {"out", "road" },
                        {"outside", "road" }
            };
        }
    }
}