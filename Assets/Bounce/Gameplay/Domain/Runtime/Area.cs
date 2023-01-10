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
        readonly float speedBoost;

        public bool InsideBounds(Vector2 end) => bounds.Contains(end);
        public bool Drawing => sketchbook.Drawing;
        public Bounds2D Bounds => bounds;

        public Area(Bounds2D bounds, float minTrampolineLength, float maxTrampolineLength, float speedBoost)
        {
            sketchbook = new Sketchbook { Bounds = bounds, MinTrampolineLength = minTrampolineLength, MaxTrampolineLength = maxTrampolineLength};
            this.bounds = bounds;
            this.speedBoost = speedBoost;
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
            
            var trajectoryCollision = ball.Collision(previousBallPosition, trampoline);

            if(trajectoryCollision == Vector2.Null)
                return;
            
            ball.Orientation = trampoline.Reflect(ball.Orientation);
            var bounceMagnitude = previousBallPosition.To(ball.Position).Magnitude -
                                  previousBallPosition.To(trajectoryCollision).Magnitude;
            ball.Position = trajectoryCollision + bounceMagnitude * ball.Orientation;
            ball.Speed += speedBoost;
        }
    }
}