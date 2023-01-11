using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Application.Runtime
{
    public class Gameplay
    {
        readonly PlayersController playersController;
        readonly DropBall dropBall;
        readonly MoveBall moveBall;
        readonly Game game;
        
        public Gameplay(PlayersController playersController, DropBall dropBall, MoveBall moveBall, Game game)
        {
            this.playersController = playersController;
            this.dropBall = dropBall;
            this.moveBall = moveBall;
            this.game = game;
        }

        public async Task Play(CancellationToken cancellationToken)
        {
            game.Begin();
            playersController.EnablePlayers();
            await dropBall.Run(cancellationToken);
            while (game.Playing)
            {
                moveBall.Simulate(0.016f);
                await Task.Delay(16, cancellationToken);
            }
            
            playersController.DisablePlayers();
        }

        public void Quit()
        {
            game.End();
        }
    }
}