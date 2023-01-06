using System;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Area
    {
        public Trampoline Trampoline => trampoline;
        public float MinTrampolineSize { get; init; } = 0;
        
        Trampoline trampoline = Trampoline.Null;
        
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
            if(!Drawing && bounds.Contains(end))
                sketchbook.Draw(end);
            else if(Drawing)
                sketchbook.Draw(bounds.ClampWithRaycast(trampoline.Origin, end));

            trampoline = sketchbook.Result;
        }

        public void StopDrawing()
        {
            if(sketchbook.Result.Origin == sketchbook.Result.End)
            {
                trampoline = Trampoline.Null;
            }
            else
            {
                trampoline = sketchbook.Result;

                if(trampoline.Origin.To(trampoline.End).Magnitude < MinTrampolineSize)
                {
                    var extraSize = MinTrampolineSize - trampoline.Origin.To(trampoline.End).Magnitude;

                    trampoline = new Trampoline
                    {
                        Origin = trampoline.Origin + trampoline.End.To(trampoline.Origin).Normalize * extraSize / 2,
                        End = trampoline.End + trampoline.Origin.To(trampoline.End).Normalize * extraSize / 2
                    }; 
                }
            }

            sketchbook.StopDrawing();
        }

        public void HandleCollision(Ball ball, Vector2 previousBallPosition)
        {
            var trajectory = new Segment(previousBallPosition, ball.Position);
            var collisionPoint = trampoline.CollisionPoint(trajectory);
            
            if(collisionPoint != Vector2.Null)
            {
                var bounceMagnitude = (ball.Position - collisionPoint).Magnitude;
                ball.Orientation = trampoline.Reflect(ball.Orientation);
                ball.Position = collisionPoint + ball.Orientation * bounceMagnitude;
            }
        }
    }
}