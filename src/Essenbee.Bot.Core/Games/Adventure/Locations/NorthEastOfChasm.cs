﻿using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public partial class NorthEastOfChasm : AdventureLocation
    {
        public NorthEastOfChasm(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.NorthEastOfChasm;
            Name = "North East Side of a Chasm";
            ShortDescription = "on the northeast side of a chasm";
            LongDescription = "on one side of a large, deep chasm. A heavy white mist rising " +
                              "up from below obscures all view of the far side.  A NE path leads away " +
                              "from the chasm into a narrow E/W passage. ";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.TrollBridge, "southwest", "sw", "down", "d"),
                // new PlayerMove(string.Empty, Location., "northeast", "ne"), // Fork
            };
        }
    }
}
