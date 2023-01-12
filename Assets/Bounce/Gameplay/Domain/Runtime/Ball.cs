using System;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Ball
    {
        public Vector2 Position { get; set; }
        public float Speed { get; set; } = 1;
        
        Vector2 orientation;
        
        public float Diameter { get; }
        public float Radius => Diameter / 2;
        public Vector2 Velocity => Speed * Orientation;

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

        public void MoveForward(float movement)
        {
            Position += movement * Orientation;
        }

        public Vector2 Collision(Vector2 previousPosition, Trampoline trampoline)
        {
            var movement = new Segment(previousPosition, Position);
            var collision0 = movement.CollisionTo(trampoline.Segment.Pararel0(Radius));
            var collision1 = movement.CollisionTo(trampoline.Segment.Pararel1(Radius));

            return collision0 != Vector2.Null ? collision0 : collision1;
        }

        public static Ball Null => null;

        public Ball Copy()
        {
            return new Ball(Position, orientation, Diameter) { Speed = Speed };
        }
    }
}