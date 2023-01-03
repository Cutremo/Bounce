using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Application.Runtime
{
    public class Gameplay
    {
        readonly PlayersController playersController;
        readonly DropBall dropBall;
        readonly Game game;
        public Gameplay(PlayersController playersController, DropBall dropBall, Game game)
        {
            this.playersController = playersController;
            this.dropBall = dropBall;
            this.game = game;
        }

        public async Task Play()
        {
            game.Begin();
            playersController.EnablePlayers();
            await dropBall.Run();
            while (game.Playing)
            {
                await Task.Yield();
            }
            playersController.DisablePlayers();
        }
    }
}