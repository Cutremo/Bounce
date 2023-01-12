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
        readonly MoveBall moveBall;
        readonly PointController pointController;
        readonly Game game;
        
        public Gameplay(PlayersController playersController, MoveBall moveBall, PointController pointController, Game game)
        {
            this.playersController = playersController;
            this.moveBall = moveBall;
            this.pointController = pointController;
            this.game = game;
        }

        public async Task Play(CancellationToken cancellationToken)
        {
            game.Begin();
            playersController.EnablePlayers();
            await pointController.BeginPoint(cancellationToken);
            while (game.Playing)
            {
                moveBall.Simulate(0.016f);
                await Task.Delay(16, cancellationToken);
                if (!game.PlayingPoint)
                    await pointController.EndPoint(cancellationToken);
            }

            playersController.DisablePlayers();
        }

        public void Quit()
        {
            game.End();
        }
    }
}