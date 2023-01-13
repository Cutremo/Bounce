using System.Threading;
using System.Threading.Tasks;
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
        readonly SketchbookView sketchbookView;
        readonly TrampolinesView trampolinesView;
        readonly DrawTrampolineInput drawingInput;
        public DrawTrampoline(Game game, Player player, SketchbookView sketchbookView, TrampolinesView trampolinesView, DrawTrampolineInput drawingInput)
        {
            this.game = game;
            this.player = player;
            this.sketchbookView = sketchbookView;
            this.trampolinesView = trampolinesView;
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

        //Mover a otro controlador?
        public Task Clear(CancellationToken cancellationToken)
        {
            sketchbookView.StopDrawing();
            trampolinesView.RemoveCurrent();
            return Task.CompletedTask;
        }

        void Draw(Vector2 position)
        {
            if(!game.InsideBounds(player, position))
                return;

            if(!game.IsDrawing(player))
            {
                trampolinesView.RemoveCurrent();
                sketchbookView.BeginDraw(position);
            }
            game.Draw(player, position);
            sketchbookView.Draw(game.TrampolineOf(player));
        }

        void EndDraw()
        {
            if(!game.IsDrawing(player))
            {
                Debug.LogWarning("Ended Drawing when not drawing. Handled.");
                return;
            }
            
            game.StopDrawing(player);
            if(game.TrampolineOf(player) != Trampoline.Null)
                trampolinesView.Add(game.TrampolineOf(player));
            
            sketchbookView.StopDrawing();
        }
    }
}
