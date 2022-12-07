using JunityEngine.Maths.Runtime;

namespace Bounce.Runtime.Domain
{
    public partial record Trampoline 
    {
        public Vector2 Origin { get; set; }
        public Vector2 End { get; set; }
    }
}