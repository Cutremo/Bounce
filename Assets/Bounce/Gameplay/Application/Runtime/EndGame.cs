using System;
using System.Threading;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public class EndGame
    {
        readonly Game game;

        public EndGame(Game game, CancellationTokenSource cancellationTokenSource)
        {
            this.game = game;
        }

        public void Run()
        {
            game.End();
        }
    }
}