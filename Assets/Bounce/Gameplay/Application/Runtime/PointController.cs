using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Application.Runtime
{
    public class PointController
    {
        readonly Game game;
        readonly BallRef ballRef;
        readonly PlayersController playersController;
        readonly BallsView ballsView;

        public PointController(Game game, BallRef ballRef, PlayersController playersController, BallsView ballsView)
        {
            this.game = game;
            this.ballRef = ballRef;
            this.playersController = playersController;
            this.ballsView = ballsView;
        }
        public async Task EndPoint(CancellationToken cancellationToken)
        {
            playersController.DisablePlayers();

            await Task.WhenAll(playersController.ClearPlayers(cancellationToken), ballsView.RemoveBall(cancellationToken));
            await Task.Delay(2000, cancellationToken);
        }

        public async Task BeginPoint(CancellationToken cancellationToken)
        {
            await ballRef.DropBall(cancellationToken);
            game.BeginPoint();
            playersController.EnablePlayers();
        }
    }
}