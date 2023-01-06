using System;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Area
    {
        public Trampoline Trampoline => trampoline;
        
        Trampoline trampoline = Trampoline.Null;
        
        readonly Sketchbook sketchbook;
        readonly Bounds2D bounds;

        public bool InsideBounds(Vector2 end) => bounds.Contains(end);
        public bool Drawing => sketchbook.Drawing;
        public Bounds2D Bounds => bounds;

        public Area(Bounds2D bounds, float minTrampolineLength, float maxTrampolineLength)
        {
            sketchbook = new Sketchbook { Bounds = bounds, MinTrampolineLength = minTrampolineLength, MaxTrampolineLength = maxTrampolineLength};
            this.bounds = bounds;
        }

        public void Draw(Vector2 end)
        {
            sketchbook.DrawClamped(end);
            trampoline = sketchbook.Wip;
        }

        public void StopDrawing()
        {
            trampoline = sketchbook.Result();
            sketchbook.StopDrawing();
        }

        public void HandleCollision(Ball ball, Vector2 previousBallPosition)
        {
            Contract.Require(trampoline.Completed).True();
            
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