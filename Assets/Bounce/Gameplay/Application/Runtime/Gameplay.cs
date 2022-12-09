using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Application.Runtime
{
    public class Gameplay
    {
        readonly DrawTrampoline drawTrampoline;
        readonly Game game;
        public Gameplay(DrawTrampoline drawTrampoline, Game game)
        {
            this.drawTrampoline = drawTrampoline;
            this.game = game;
        }

        public async Task Play()
        {
            game.Begin();
            drawTrampoline.AllowDraw();
            while (game.Playing)
            {
                await Task.Yield();
            }
            drawTrampoline.DisallowDraw();
        }

        //Empezar.
    }
}