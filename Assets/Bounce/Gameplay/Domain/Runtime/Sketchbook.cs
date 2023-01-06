using JunityEngine;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Sketchbook
    {
        public Bounds2D Bounds { get; init; } = Bounds2D.Infinite;
        public int MaxTrampolineLength { get; init; } = int.MaxValue;
        public Trampoline Result { get; private set; } = Trampoline.Null;
        public bool Drawing => Result is not INull;

        public void Draw(Vector2 end)
        {
            if (!Drawing) BeginDraw(end);

            Result.Origin  = CalcOrigin(end);
            Result.End  = end;
        }

        Vector2 CalcOrigin(Vector2 end)
        {
            return Result.Origin.To(end).Magnitude > MaxTrampolineLength
                ? end + (end.To(Result.Origin).Normalize * MaxTrampolineLength)
                : Result.Origin;
        }

        void BeginDraw(Vector2 position)
        {
            Contract.Require(Drawing).False();
            Result = new Trampoline{ Origin = position, End = position};
        }

        public void StopDrawing()
        { 
            Contract.Require(Drawing).True();
            Result = Trampoline.Null;
        }
    }
}