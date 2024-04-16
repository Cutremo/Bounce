using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public partial record Trampoline
    {
        public Vector2 Origin { get; set; }
        public Vector2 End { get; set; }

        public bool Completed => Origin != End;
        public Segment Segment => new(Origin, End);
        
        public Vector2 Reflect(Vector2 ballOrientation)
        {
            return new Segment(Origin, End).Reflect(ballOrientation);
        }
    }
}