using System;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Area
    {
        public Trampoline Trampoline => trampoline;
        
        Trampoline trampoline = Trampoline.Null;
        
        readonly Sketchbook sketchbook;
        readonly Bounds2D bounds;
        readonly Vector2 scoringDirection;
        readonly float speedBoost;

        public bool InsideBounds(Vector2 end) => bounds.Contains(end);
        public bool Drawing => sketchbook.Drawing;
        public Bounds2D Bounds => bounds;
        public event Action TrampolineCollided;

        public Area(Bounds2D bounds, Vector2 scoringDirection, float minTrampolineLength, float maxTrampolineLength, float speedBoost)
        {
            Require(scoringDirection.Cardinalized).True();
            sketchbook = new Sketchbook { Bounds = bounds, MinTrampolineLength = minTrampolineLength, MaxTrampolineLength = maxTrampolineLength};
            this.bounds = bounds;
            this.scoringDirection = scoringDirection;
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

        public bool HandleCollision(Ball ball, Vector2 previousBallPosition)
        {
            Require(trampoline.Completed).True();
            
            var trajectoryCollision = ball.Collision(previousBallPosition, trampoline);

            if(trajectoryCollision == Vector2.Null)
                return false;
            
            BounceBall(ball, previousBallPosition, trajectoryCollision);
            
            TrampolineCollided?.Invoke();
            return true;
        }

        private void BounceBall(Ball ball, Vector2 previousBallPosition, Vector2 trajectoryCollision)
        {
            ball.Orientation = trampoline.Reflect(ball.Orientation);
            var bounceMagnitude = previousBallPosition.To(ball.Position).Magnitude -
                                  previousBallPosition.To(trajectoryCollision).Magnitude;
            ball.Position = trajectoryCollision + bounceMagnitude * ball.Orientation;
            ball.Speed += speedBoost;
            ball.Bounce();
        }

        public bool Scores(Ball ball)
        {
            return bounds.IsOutsideOnDirection(ball.Position - ball.Radius * scoringDirection, scoringDirection);
        }

        public void Clear()
        {
            trampoline = Trampoline.Null;
        }
    }
}