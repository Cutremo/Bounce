using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public class EndPoint
    {
        readonly Game game;
        readonly BallRef ballRef;
        readonly List<DrawTrampoline> players;
        readonly BallsView ballsView;

        CancellationToken cancellationToken;
        public EndPoint(Game game, BallRef ballRef, List<DrawTrampoline> players, BallsView ballsView)
        {
            this.game = game;
            this.ballRef = ballRef;
            this.players = players;
            this.ballsView = ballsView;
        }
        public async Task Run()
        {
            game.ClearPitch();
            var clearTasks = players.Select(player => player.Clear(cancellationToken)).ToList();
            clearTasks.Add(ballsView.RemoveBall(cancellationToken));

            await Task.WhenAll(clearTasks);
        }
    }
}