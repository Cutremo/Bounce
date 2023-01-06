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

        Trampoline trampoline = Trampoline.Null;

        public Trampoline WIP
        {
            get
            {
                return trampoline;
            }
        }
        public bool Drawing => trampoline is not INull;

        public void DrawClamped(Vector2 end)
        {
            if(!Drawing && Bounds.Contains(end))
                Draw(end);
            else if(Drawing)
                Draw(Bounds.ClampWithRaycast(trampoline.Origin, end));
        }
        public void Draw(Vector2 end)
        {
            if (!Drawing) BeginDraw(end);

            trampoline.Origin  = CalcOrigin(end);
            trampoline.End  = end;
        }

        Vector2 CalcOrigin(Vector2 end)
        {
            return trampoline.Origin.To(end).Magnitude > MaxTrampolineLength
                ? end + (end.To(trampoline.Origin).Normalize * MaxTrampolineLength)
                : trampoline.Origin;
        }

        void BeginDraw(Vector2 position)
        {
            Contract.Require(Drawing).False();
            trampoline = new Trampoline{ Origin = position, End = position};
        }

        public void StopDrawing()
        { 
            Contract.Require(Drawing).True();
            trampoline = Trampoline.Null;
        }

        public Trampoline Result()
        {
            var returnValue = trampoline;
            if(WIP.Origin == WIP.End)
            {
                returnValue = Trampoline.Null;
            }
            else
            {
                returnValue = WIP;

                if(returnValue.Origin.To(returnValue.End).Magnitude < MinTrampolineLength)
                {
                    var extraSize = MinTrampolineLength - returnValue.Origin.To(returnValue.End).Magnitude;

                    returnValue = new Trampoline
                    {
                        Origin = returnValue.Origin + returnValue.End.To(returnValue.Origin).Normalize * extraSize / 2,
                        End = returnValue.End + returnValue.Origin.To(returnValue.End).Normalize * extraSize / 2
                    }; 
                }
            }

            return returnValue;
        }
    }
}