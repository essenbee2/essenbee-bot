﻿using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SouthSideChamber : AdventureLocation
    {
        public SouthSideChamber(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SouthSideChamber;
            Name = "South Side Chamber";
            ShortDescription = "in south side chamber";
            LongDescription = "in the south side chamber of the Hall of the Mountain King.";
            Level = 1;
            IsDark = true;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Jewelry) };
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove("Your footsetps echo around you...", Location.HallOfMountainKing, "north", "n"),

            };
        }
    }
}
