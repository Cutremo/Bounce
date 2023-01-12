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
        readonly BallRef ballRef;
        readonly MoveBall moveBall;
        readonly EndPoint endPoint;
        readonly Game game;
        
        public Gameplay(PlayersController playersController, BallRef ballRef, MoveBall moveBall, EndPoint endPoint, Game game)
        {
            this.playersController = playersController;
            this.ballRef = ballRef;
            this.moveBall = moveBall;
            this.endPoint = endPoint;
            this.game = game;
        }

        public async Task Play(CancellationToken cancellationToken)
        {
            game.Begin();
            playersController.EnablePlayers();
            await ballRef.DropBall(cancellationToken);
            while (game.PlayingPoint)
            {
                moveBall.Simulate(0.016f);
                await Task.Delay(16, cancellationToken);
            }
            if(game.Playing)
                await endPoint.Run();

            playersController.DisablePlayers();
        }

        public void Quit()
        {
            game.End();
        }
    }
}