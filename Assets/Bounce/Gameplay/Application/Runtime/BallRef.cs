using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public class BallRef
    {
        readonly Game game;
        readonly BallsView ballsView;
        readonly Ball ball;
        
        public BallRef(Game game, Ball ball, BallsView ballsView)
        {
            this.game = game;
            this.ballsView = ballsView;
            this.ball = ball;
        }

        public async Task DropBall(CancellationToken cancellationToken)
        {
            var instance = ball.Copy();
            await ballsView.DropBall(instance, cancellationToken);
            game.DropBall(instance);
        }

        public async Task RemoveBall(Ball gameBall, CancellationToken ct)
        {
            game.RemoveBall();
            await ballsView.RemoveBall(gameBall, ct);
        }
    }
}