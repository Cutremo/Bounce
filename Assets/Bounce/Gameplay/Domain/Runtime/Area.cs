using System;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Area
    {
        public Trampoline Trampoline => trampoline;
        public float MinTrampolineSize { get; init; } = 0;
        
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

            if(trampoline.Origin.To(trampoline.End).Size < MinTrampolineSize)
            {
                var extraSize = MinTrampolineSize - trampoline.Origin.To(trampoline.End).Size;

                trampoline = new Trampoline
                {
                    Origin = trampoline.Origin + trampoline.End.To(trampoline.Origin).Normalize * extraSize / 2,
                    End = trampoline.End + trampoline.Origin.To(trampoline.End).Normalize * extraSize / 2
                }; 
            }

            
            sketchbook.StopDrawing();
        }
    }
}