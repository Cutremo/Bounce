using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Domain.Tests.Builders
{
    public class BallBuilder
    {
        Vector2 position = Vector2.Zero;
        Vector2 orientation = Vector2.Down.Normalize;
        float diameter = 0;
        float speed = 4;
        
        public static BallBuilder Ball() => new();
        public Ball Build() => new Ball(position, orientation, diameter, speed);

        public BallBuilder WithDiameter(float diameter)
        {
            this.diameter = diameter;
            return this;
        }
        
        public BallBuilder WithSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }
        
        public BallBuilder WithOrientation(Vector2 orientation)
        {
            this.orientation = orientation;
            return this;
        }

        public BallBuilder AtTheCenterOf(Pitch pitch)
        {
            position = pitch.Center;
            return this;
        }
    }
}