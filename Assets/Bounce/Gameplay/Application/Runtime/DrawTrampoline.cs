using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Input.Runtime;
using UnityEngine;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Application.Runtime
{
    public class DrawTrampoline 
    {
        readonly Game game;
        readonly DrawLineView drawLineView;
        readonly DrawTrampolineInput drawingInput;
        
        bool drawing; //Mover a dominio
        
        public DrawTrampoline(Game game, DrawLineView drawLineView, DrawTrampolineInput drawingInput)
        {
            this.game = game;
            this.drawLineView = drawLineView;
            this.drawingInput = drawingInput;
        }

        public void AllowDraw()
        {
            if (drawing) return;
            drawing = true;
            drawingInput.DrawInputReceived += Draw;
            drawingInput.EndDrawInputReceived += EndDraw;
        }
        
        public void DisallowDraw()
        {
            if (!drawing) return;
            drawing = false;
            drawingInput.DrawInputReceived -= Draw;
            drawingInput.EndDrawInputReceived -= EndDraw;
        }
        
        void Draw(DrawInputReceivedArgs args)
        {
            if (!game.InsideBounds(args.Actor, args.Position))
                return;
            
            game.Draw(args.Actor, args.Position);
            drawLineView.Draw(game.TrampolineOf(args.Actor));
        }

        void EndDraw(Player player)
        {
            if (!game.IsDrawing(player))
                return;
            
            game.StopDrawing(player);
            drawLineView.StopDrawing(game.TrampolineOf(player));
        }
    }
}
