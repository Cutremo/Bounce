using JunityEngine;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Sketchbook
    {
        public Bounds2D Bounds { get; init; } = Bounds2D.Infinite;
        public float MaxTrampolineLength { get; init; } = float.MaxValue;
        public float MinTrampolineLength { get; init; } = 0;

        Trampoline wip = Trampoline.Null;

        public Trampoline Wip => wip;
        public bool Drawing => wip is not INull;

        public void DrawClamped(Vector2 end)
        {
            if(!Drawing && Bounds.Contains(end))
                Draw(end);
            else if(Drawing)
                Draw(Bounds.ClampWithRaycast(wip.Origin, end));
        }
        public void Draw(Vector2 end)
        {
            if (!Drawing) BeginDraw(end);

            wip.Origin  = CalcOrigin(end);
            wip.End  = end;
        }

        Vector2 CalcOrigin(Vector2 end)
        {
            return wip.Origin.To(end).Magnitude > MaxTrampolineLength
                ? end + (end.To(wip.Origin).Normalize * MaxTrampolineLength)
                : wip.Origin;
        }

        void BeginDraw(Vector2 position)
        {
            Contract.Require(Drawing).False();
            wip = new Trampoline{ Origin = position, End = position};
        }

        public void StopDrawing()
        { 
            Contract.Require(Drawing).True();
            wip = Trampoline.Null;
        }

        public Trampoline Result()
        {
            return wip.Completed ? WipWithAtLeastMinSize() : Trampoline.Null;
        }

        Trampoline WipWithAtLeastMinSize()
        {
            if(wip.Origin.To(wip.End).Magnitude > MinTrampolineLength)
                return wip;
            
            var extraSize = MinTrampolineLength - wip.Origin.To(wip.End).Magnitude;

            return new Trampoline
            {
                Origin = wip.Origin + wip.End.To(wip.Origin).Normalize * extraSize / 2,
                End = wip.End + wip.Origin.To(wip.End).Normalize * extraSize / 2
            };
        }
    }
}