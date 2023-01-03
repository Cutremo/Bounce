using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public class MoveBall
    {
        readonly Game game;
        readonly BallsView ballsView;

        public MoveBall(Game game, BallsView ballsView)
        {
            this.game = game;
            this.ballsView = ballsView;
        }

        public void Simulate(float seconds)
        {
            game.SimulateBall(seconds);
            ballsView.MoveBall(game.Ball);
        }
    }
}