using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Application.Runtime
{
    public class PlayerDrawing
    {
        readonly Game game;
        readonly DrawTrampoline[] playersActions;

        public PlayerDrawing(Game game, List<DrawTrampoline> playersActions)
        {
            this.game = game;
            this.playersActions = playersActions.ToArray();
        }

        public void EnablePlayers()
        {
            if(game.PlayersEnabled) return;
            foreach(var p in playersActions)
            {
                p.EnableDraw();
            }
            game.PlayersEnabled = true;
        }
        
        public void DisablePlayers()
        {
            if(!game.PlayersEnabled) return;
            foreach(var p in playersActions)
            {
                p.DisableDraw();
            }

            game.PlayersEnabled = false;
        }

        public Task ClearPlayers(CancellationToken cancellationToken)
        {
            return Task.WhenAll(playersActions.Select(player => player.Clear(cancellationToken)).ToList());
        }
    }
}