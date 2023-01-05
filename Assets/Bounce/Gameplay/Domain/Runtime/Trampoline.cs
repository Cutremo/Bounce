using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public partial record Trampoline 
    {
        public Vector2 Origin { get; set; }
        public Vector2 End { get; set; }

        public Vector2 CollisionPoint(Segment movement)
        {
            return movement.CollisionTo(new Segment(Origin, End));
        }
    }
}