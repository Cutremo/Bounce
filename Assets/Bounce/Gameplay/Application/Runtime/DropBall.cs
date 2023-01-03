using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public class DropBall
    {
        readonly Game game;
        readonly BallsView ballsView;

        public DropBall(Game game, BallsView ballsView)
        {
            this.game = game;
            this.ballsView = ballsView;
        }

        public async Task Run()
        {
            game.DropBall();
            await ballsView.DropBall(game.Ball);
        }
    }
}