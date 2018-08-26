﻿using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Xyzzy : BaseAdventureCommand
    {
        public Xyzzy(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var canMove = false;

            if (player.CurrentLocation.LocationId.Equals(Location.Building))
            {
                var moveTo = Location.Debris;
                canMove = _game.TryGetLocation(moveTo, out var place);
                player.CurrentLocation = place;
            }
            else if (player.CurrentLocation.LocationId.Equals(Location.Debris))
            {
                var moveTo = Location.Building;
                canMove = _game.TryGetLocation(moveTo, out var place);
                player.CurrentLocation = place;
            }

            if (canMove)
            {
                player.ChatClient.PostDirectMessage(player.Id, "You suddenly feely dizzy and space seems to warp around you!");
                return;
            }

            player.ChatClient.PostDirectMessage(player.Id, "Nothing happens...");
        }
    }
}
