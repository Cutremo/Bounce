using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Input.Runtime;
using UnityEngine;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Application.Runtime
{
    public class  DrawTrampoline 
    {
        readonly Game game;
        readonly Player player;
        readonly DrawTrampolineView drawTrampolineView;
        readonly TrampolineView trampolineView;
        readonly DrawTrampolineInput drawingInput;
        public DrawTrampoline(Game game, Player player, DrawTrampolineView drawTrampolineView, TrampolineView trampolineView, DrawTrampolineInput drawingInput)
        {
            this.game = game;
            this.player = player;
            this.drawTrampolineView = drawTrampolineView;
            this.trampolineView = trampolineView;
            this.drawingInput = drawingInput;
        }

        public void EnableDraw()
        {
            drawingInput.DrawInputReceived += Draw;
            drawingInput.EndDrawInputReceived += EndDraw;
        }
        
        public void DisableDraw()
        {
            drawingInput.DrawInputReceived -= Draw;
            drawingInput.EndDrawInputReceived -= EndDraw;
        }

        void Draw(Vector2 position)
        {
            if (!game.InsideBounds(player, position))
                return;
            
            trampolineView.RemoveCurrent(player);
            game.Draw(player, position);
            drawTrampolineView.Draw(game.TrampolineOf(player));
        }

        void EndDraw()
        {
            if (!game.IsDrawing(player))
                return;
            
            game.StopDrawing(player);
            trampolineView.Add(player, game.TrampolineOf(player));
            drawTrampolineView.StopDrawing(game.TrampolineOf(player));
        }
    }
}
