using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Input.Runtime;
using UnityEngine;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Application.Runtime
{
    public class DrawTrampoline 
    {
        readonly Game game;
        readonly DrawTrampolineView drawTrampolineView;
        readonly TrampolineView trampolineView;
        readonly DrawTrampolineInput drawingInput;
        public DrawTrampoline(Game game, DrawTrampolineView drawTrampolineView, TrampolineView trampolineView, DrawTrampolineInput drawingInput)
        {
            this.game = game;
            this.drawTrampolineView = drawTrampolineView;
            this.trampolineView = trampolineView;
            this.drawingInput = drawingInput;
        }

        public void AllowDraw()
        {
            if (game.CanDraw) return;
            drawingInput.DrawInputReceived += Draw;
            drawingInput.EndDrawInputReceived += EndDraw;
            game.CanDraw = true;
        }
        
        public void DisallowDraw()
        {
            if (!game.CanDraw) return;
            drawingInput.DrawInputReceived -= Draw;
            drawingInput.EndDrawInputReceived -= EndDraw;
            game.CanDraw = false;
        }
        
        void Draw(DrawInputReceivedArgs args)
        {
            if (!game.InsideBounds(args.Actor, args.Position))
                return;
            
            trampolineView.RemoveCurrent(args.Actor);
            game.Draw(args.Actor, args.Position);
            drawTrampolineView.Draw(game.TrampolineOf(args.Actor));
        }

        void EndDraw(Player player)
        {
            if (!game.IsDrawing(player))
                return;
            
            game.StopDrawing(player);
            trampolineView.Add(player, game.TrampolineOf(player));
            drawTrampolineView.StopDrawing(game.TrampolineOf(player));
        }
    }
}
