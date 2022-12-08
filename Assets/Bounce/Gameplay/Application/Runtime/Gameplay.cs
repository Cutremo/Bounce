using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Application.Runtime
{
    public class Gameplay
    {
        readonly DrawLine drawLine;
        readonly Game game;
        public Gameplay(DrawLine drawLine, Game game)
        {
            this.drawLine = drawLine;
            this.game = game;
        }

        public async Task Play()
        {
            game.Begin();
            while (game.Playing)
            {
                await drawLine.HandleDraw();
                await Task.Yield();
            }
        }

        //Empezar.
    }
}