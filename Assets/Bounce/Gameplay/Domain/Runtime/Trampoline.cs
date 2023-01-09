using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public partial record Trampoline
    {
        public Vector2 Origin { get; set; }
        public Vector2 End { get; set; }

        public bool Completed => Origin != End;
        public Segment Segment => new Segment(Origin, End);
        
        
        public Vector2 CollisionPoint(Vector2 previousPosition, Ball ball)
        {
            var movement = new Segment(previousPosition, ball.Position).ExtendFromBothEnds(ball.Radius);
            
            return movement.CollisionTo(Segment);
        }


        public Vector2 Reflect(Vector2 ballOrientation)
        {
            return new Segment(Origin, End).Reflect(ballOrientation);
        }
    }
}