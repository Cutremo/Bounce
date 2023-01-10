using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public partial record Trampoline
    {
        public Vector2 Origin { get; set; }
        public Vector2 End { get; set; }

        public bool Completed => Origin != End;
        public Segment Segment => new Segment(Origin, End);
        
        
        public Vector2 CollisionCenter(Vector2 previousPosition, Ball ball)
        {
            var movement = new Segment(previousPosition, ball.Position);
            var collision0 = movement.CollisionTo(Segment.Pararel0(ball.Radius));
            var collision1 = movement.CollisionTo(Segment.Pararel1(ball.Radius));

            return collision0 != Vector2.Null ? collision0 : collision1;
            
            if(!Completed || !Segment.ExtendFromBothEnds(ball.Radius).CanBeProjected(ball.Position))
                return Vector2.Null;
            
            var segmentToPoint = Segment.ExtendFromBothEnds(ball.Radius).To(ball.Position);

            if( segmentToPoint.Magnitude <= ball.Radius)
                return Segment.Project(ball.Position);

            return Vector2.Null;
        }


        public Vector2 Reflect(Vector2 ballOrientation)
        {
            return new Segment(Origin, End).Reflect(ballOrientation);
        }
    }
}