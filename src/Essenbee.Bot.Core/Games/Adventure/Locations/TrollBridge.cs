﻿using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public partial class SouthWestOfChasm
    {
        public class TrollBridge : AdventureLocation
        {
            public TrollBridge(IReadonlyAdventureGame game) : base(game)
            {
                LocationId = Location.TrollBridge;
                Name = "Troll Bridge";
                ShortDescription = "standing on a rickety wooden bridge";
                LongDescription = "standing on a rickety wooden bridge that spans a wide and deep chasm.";
                Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Troll) };
                ValidMoves = new List<IPlayerMove> {
                    new PlayerMove("A huge green-skinned troll squats on the bridge, blocking your way and demanding treasure to let you pass!",
                    Location.TrollBridge, "northeast", "ne"),
                    //new PlayerMove(string.Empty, Location.NorthEastOfChasm, "northeast", "ne"), // when troll pacified
                    new PlayerMove(string.Empty, Location.SouthWestOfChasm, "southwest", "sw"),
                };
            }
        }
    }
}
