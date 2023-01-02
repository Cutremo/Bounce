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
        readonly Game game;
        public Gameplay(PlayersController playersController, Game game)
        {
            this.playersController = playersController;
            this.game = game;
        }

        public async Task Play()
        {
            game.Begin();
            playersController.EnablePlayers();
            while (game.Playing)
            {
                await Task.Yield();
            }
            playersController.DisablePlayers();
        }
    }
}