using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Bounce.Gameplay.Application.Runtime
{
    public class Gameplay
    {
        readonly DrawLine drawLine;
        
        public Gameplay(DrawLine drawLine)
        {
            this.drawLine = drawLine;
        }

        public async Task Play()
        {
            while (true)
            {
                await drawLine.HandleDraw();
                await Task.Yield();
            }
        }

        public void End()
        {
            
        }
        
        //Empezar.
    }
}