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
        readonly PlayerDrawing playerDrawing;
        readonly BallsView ballsView;
        readonly ScoreView scoreView;

        public PointController(Game game, BallRef ballRef, PlayerDrawing playerDrawing, BallsView ballsView, ScoreView scoreView)
        {
            this.game = game;
            this.ballRef = ballRef;
            this.playerDrawing = playerDrawing;
            this.ballsView = ballsView;
            this.scoreView = scoreView;
        }
        public async Task EndPoint(CancellationToken ct)
        {
            playerDrawing.DisablePlayers();

            await scoreView.SetScore(game.Score, ct);
            await Task.WhenAll(playerDrawing.ClearPlayers(ct), ballsView.RemoveBall(ct));
            await Task.Delay(2000, ct);
        }

        public async Task BeginPoint(CancellationToken cancellationToken)
        {
            await ballRef.DropBall(cancellationToken);
            game.BeginPoint();
            playerDrawing.EnablePlayers();
        }
    }
}