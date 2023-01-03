using System.Collections.Generic;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Field
    {
        readonly List<Ball> balls = new();
        readonly Bounds2D bounds;
        
        public Field(Bounds2D bounds)
        {
            this.bounds = bounds;
        }

        public void MoveBall(Ball ball, float amount)
        {
            Contract.Require(balls.Contains(ball)).True();
            
            var targetPosition = BouncePosition(ball, amount);

            ball.Position = targetPosition;
        }

        Vector2 BouncePosition(Ball ball, float amount)
        {
            var idealPosition = ball.Position + ball.Orientation * amount;
            var targetPosition = idealPosition;
            
            if(bounds.OnRight(idealPosition + new Vector2(ball.Radius, 0)))
                targetPosition = idealPosition.WithX(bounds.RightEdge - ball.Radius - idealPosition.X + bounds.RightEdge);
            else if(bounds.OnLeft(idealPosition - new Vector2(ball.Radius, 0)))
                targetPosition = idealPosition.WithX(bounds.LeftEdge + ball.Radius - idealPosition.X + bounds.LeftEdge);
            
            return targetPosition;
        }

        public IEnumerable<Ball> Balls => balls;

        public void DropBall(Ball ball)
        {
            Contract.Require(bounds.Contains(ball.Position)).True();
            balls.Add(ball);
        }
    }
}