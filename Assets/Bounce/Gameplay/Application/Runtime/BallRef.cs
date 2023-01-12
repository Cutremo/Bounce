using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public class BallRef
    {
        readonly Game game;
        readonly BallsView ballsView;

        public BallRef(Game game, BallsView ballsView)
        {
            this.game = game;
            this.ballsView = ballsView;
        }

        public async Task DropBall(CancellationToken cancellationToken)
        {
            game.DropBall();
            await ballsView.DropBall(game.Ball, cancellationToken);
        }
    }
}