using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public class MoveBall
    {
        readonly Game game;
        readonly BallsView ballsView;
        readonly CollisionView collisionView;

        public MoveBall(Game game, BallsView ballsView, CollisionView collisionView)
        {
            this.game = game;
            this.ballsView = ballsView;
            this.collisionView = collisionView;
        }

        public async Task Simulate(float seconds, CancellationToken ct)
        {
            await Task.Delay((int)(seconds * 1000), ct);

            game.SimulateBall(seconds);
            if(game.CollidedWithTrampoline)
            {
                game.HandleCollision();
                await collisionView.HandleCollision(game.Ball);
            }
            if(game.Ball != Ball.Null)
                await ballsView.MoveBall(game.Ball, ct);
        }
    }
}