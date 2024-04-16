using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Input.Runtime;

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
            area.TrampolineCollided += EndDrawingAndRemoveCurrent;
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

        public Task Clear(CancellationToken ct)
        {
            EndDrawingAndRemoveCurrent();
            return Task.Delay(300, ct);
        }

        void Draw(Vector2 position)
        {
            if(!area.InsideBounds(position))
                return;

            if(!area.Drawing)
            {
                trampolinesView.RemoveCurrent();
                sketchbookView.BeginDraw(position);
            }
            area.Draw(position);
            sketchbookView.Draw(area.Trampoline);
        }

        void EndDrawingAndRemoveCurrent()
        {
            EndDraw();
            area.Clear();
            trampolinesView.RemoveCurrent();
        }

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
