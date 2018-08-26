﻿namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Move : BaseAdventureCommand
    {
        public Move(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var canMove = false;
            var direction = e.ArgsAsList[1].ToLower();

            if (player.CurrentLocation.Moves.ContainsKey(direction))
            {
                var moveTo = player.CurrentLocation.Moves[direction];
                canMove = _game.TryGetLocation(moveTo, out var place);
                player.CurrentLocation = place;
            }

            if (canMove)
            {
                player.ChatClient.PostDirectMessage(player.Id, "*" + player.CurrentLocation.Name + "*");
            }
            else
            {
                player.ChatClient.PostDirectMessage(player.Id, "You cannot go in that direction!");
            }
        }
    }
}