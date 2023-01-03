using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Ball
    {
        public Vector2 Position { get; set; }
        
        Vector2 orientation;
        
        public float Diameter { get; }
        public float Radius => Diameter / 2;

        public Vector2 Orientation
        {
            get => orientation;
            set
            {
                Contract.Require(value.Normalized).True();
                orientation = value;
            }
        }

        public Ball(Vector2 position)
        {
            Position = position;
        }
        
        public Ball(Vector2 position, Vector2 orientation, float diameter = 0) : this(position)
        {
            Orientation = orientation;
            Diameter = diameter;
        }
    }
}