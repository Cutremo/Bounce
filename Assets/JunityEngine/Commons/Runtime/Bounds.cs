using RGV.DesignByContract.Runtime;

namespace JunityEngine.Maths.Runtime
{
    public struct Bounds2D
    {
        public static Bounds2D Infinite => new Bounds2D(Vector2.NegativeInfinite, Vector2.Infinite);
        public float LeftEdge => lowerBounds.X;
        public float RightEdge => upperBounds.X;
        public Vector2 Center => lowerBounds + (upperBounds - lowerBounds) / 2;

        readonly Vector2 lowerBounds;
        readonly Vector2 upperBounds;

        public Bounds2D(Vector2 lowerBounds, Vector2 upperBounds)
        {
            Contract.Require(lowerBounds.X).LesserThan(upperBounds.X);
            Contract.Require(lowerBounds.Y).LesserThan(upperBounds.Y);
            
            this.lowerBounds = lowerBounds;
            this.upperBounds = upperBounds;
        }

        public readonly bool Contains(Vector2 position)
        {
            return position.X >= lowerBounds.X &&
                   position.X <= upperBounds.X &&
                   position.Y >= lowerBounds.Y &&
                   position.Y <= upperBounds.Y;
        }

        public readonly bool OnLeft(Vector2 position) => position.X < lowerBounds.X;
        public readonly bool OnRight(Vector2 position) => position.X > upperBounds.X;

        public readonly bool Contains(Bounds2D bounds) => Contains(bounds.upperBounds) && Contains(bounds.lowerBounds);

        public readonly Vector2 Clamp(Vector2 position)
        {
            if(position.X < lowerBounds.X)
                position = position.WithX(lowerBounds.X);
            if(position.X > upperBounds.X)
                position = position.WithX(upperBounds.X);
            if(position.Y < lowerBounds.Y)
                position = position.WithY(lowerBounds.Y);
            if(position.Y > upperBounds.Y)
                position = position.WithY(upperBounds.Y);

            return position;
        }
    }
}