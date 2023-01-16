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
        readonly MoveBall moveBall;
        readonly PointController pointController;
        readonly Game game;
        
        public Gameplay(MoveBall moveBall, PointController pointController, Game game)
        {
            this.moveBall = moveBall;
            this.pointController = pointController;
            this.game = game;
        }

        public async Task Play(CancellationToken cancellationToken)
        {
            try
            {
                game.Begin();
                while (game.Playing)
                {
                    await pointController.BeginPoint(cancellationToken);

                    while(game.PlayingPoint)
                        await moveBall.Simulate(0.016f, cancellationToken);
                    
                    await pointController.EndPoint(cancellationToken);
                }
            }
            catch(OperationCanceledException e)
            {
            }
        }
    }
}