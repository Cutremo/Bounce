using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Area
    {
        public Trampoline Trampoline => trampoline;
        
        Trampoline trampoline;
        
        readonly Sketchbook sketchbook;
        readonly Bounds2D bounds;

        public bool InsideBounds(Vector2 end) => bounds.Contains(end);
        public bool Drawing => sketchbook.Drawing;
        public Bounds2D Bounds => bounds;

        public Area(Sketchbook sketchbook, Bounds2D bounds)
        {
            this.sketchbook = sketchbook;
            this.bounds = bounds;
        }

        public void Draw(Vector2 end)
        {
            Contract.Require(InsideBounds(end)).True();
            sketchbook.Draw(end);
            trampoline = sketchbook.Result;
        }

        public void StopDrawing()
        {
            trampoline = sketchbook.Result;
            sketchbook.StopDrawing();
        }
    }
}