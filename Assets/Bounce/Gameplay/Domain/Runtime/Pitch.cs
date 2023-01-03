using System.Collections.Generic;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Pitch
    {
        readonly List<Ball> balls = new();
        readonly Bounds2D bounds;
        public Vector2 Center => bounds.Center;
        public Pitch(Bounds2D bounds)
        {
            this.bounds = bounds;
        }

        public void MoveBall(Ball ball, float seconds)
        {
            Contract.Require(balls.Contains(ball)).True();
            
            var idealPosition = ball.Position + ball.Orientation * seconds * ball.Speed;

            var targetPosition = PositionAfterCollisions(idealPosition, ball.Radius);

            if(idealPosition != targetPosition)
                ball.Orientation = ball.Orientation.SymmetricOnYAxis;

            ball.Position = targetPosition;
        }

        Vector2 PositionAfterCollisions(Vector2 idealPosition, float radius)
        {
            var targetPosition = idealPosition;
            
            if(bounds.OnRight(idealPosition + new Vector2(radius, 0)))
                targetPosition = idealPosition.WithX(bounds.RightEdge - radius - idealPosition.X + bounds.RightEdge);
            else if(bounds.OnLeft(idealPosition - new Vector2(radius, 0)))
                targetPosition = idealPosition.WithX(bounds.LeftEdge + radius - idealPosition.X + bounds.LeftEdge);
            
            return targetPosition;
        }

        public IEnumerable<Ball> Balls => balls;

        public void DropBall(Ball ball)
        {
            Contract.Require(bounds.Contains(ball.Position)).True();
            balls.Add(ball);
        }

        public bool Contains(Area area) => bounds.Contains(area.Bounds);
    }
}