using System.Collections.Generic;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Pitch
    {
        public Ball Ball { get; private set; }
        
        readonly Bounds2D bounds;
        public Vector2 Center => bounds.Center;
        public Pitch(Bounds2D bounds)
        {
            this.bounds = bounds;
        }

        public void SimulateBall(float seconds)
        {
            Contract.Require(this.Ball).Not.Null();
            
            var idealPosition = Ball.Position + Ball.Orientation * seconds * Ball.Speed;

            var targetPosition = PositionAfterCollisions(idealPosition, Ball);

            if(idealPosition != targetPosition)
                Ball.Orientation = Ball.Orientation.SymmetricOnYAxis;

            Ball.Position = targetPosition;
        }

        Vector2 PositionAfterCollisions(Vector2 idealPosition, Ball ball)
        {
            var targetPosition = idealPosition;
            
            if(bounds.OnRight(idealPosition + new Vector2(ball.Radius, 0)))
                targetPosition = idealPosition.WithX(bounds.RightEdgeX - ball.Diameter - idealPosition.X + bounds.RightEdgeX);
            else if(bounds.OnLeft(idealPosition - new Vector2(ball.Radius, 0)))
                targetPosition = idealPosition.WithX(bounds.LeftEdgeX + ball.Diameter - idealPosition.X + bounds.LeftEdgeX);
            
            return targetPosition;
        }
        
        
        public void DropBall(Ball ball)
        {
            Contract.Require(bounds.Contains(ball.Position)).True();
            Contract.Require(this.Ball == null).True();

            this.Ball = ball;
        }

        public bool Contains(Area area) => bounds.Contains(area.Bounds);
    }
}