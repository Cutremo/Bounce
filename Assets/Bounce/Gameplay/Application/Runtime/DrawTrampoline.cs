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
        readonly Area area;
        readonly SketchbookView sketchbookView;
        readonly TrampolinesView trampolinesView;
        readonly DrawTrampolineInput drawingInput;
        public DrawTrampoline(Area area, SketchbookView sketchbookView, TrampolinesView trampolinesView, DrawTrampolineInput drawingInput)
        {
            this.area = area;
            this.sketchbookView = sketchbookView;
            this.trampolinesView = trampolinesView;
            this.drawingInput = drawingInput;
            area.TrampolineDestroyed += EndDrawingAndRemoveCurrent;
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
            if(!area.InsideBounds(position))
                return;

            if(!area.Drawing)
            {
                RemoveCurrent();
                sketchbookView.BeginDraw(position);
            }
            area.Draw(position);
            sketchbookView.Draw(area.Trampoline);
        }

        void EndDrawingAndRemoveCurrent()
        {
            EndDraw();
            RemoveCurrent();
        }

        void RemoveCurrent() => trampolinesView.RemoveCurrent();

        void EndDraw()
        {
            if(!area.Drawing)
                return;

            area.StopDrawing();
            if(area.Trampoline != Trampoline.Null)
                trampolinesView.Add(area.Trampoline);
            
            sketchbookView.StopDrawing();
        }
    }
}
