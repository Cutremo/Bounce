using RGV.DesignByContract.Runtime;

namespace JunityEngine.Maths.Runtime
{
    public struct Bounds2D
    {
        public static Bounds2D Infinite => new Bounds2D(Vector2.NegativeInfinite, Vector2.Infinite);
        
        readonly Vector2 lowerBounds;
        readonly Vector2 upperBounds;

        public Bounds2D(Vector2 lowerBounds, Vector2 upperBounds)
        {
            Contract.Require(lowerBounds.X).LesserThan(upperBounds.X);
            Contract.Require(lowerBounds.Y).LesserThan(upperBounds.Y);
            
            this.lowerBounds = lowerBounds;
            this.upperBounds = upperBounds;
        }

        public bool Contains(Vector2 position)
        {
            return position.X >= lowerBounds.X &&
                   position.X <= upperBounds.X &&
                   position.Y >= lowerBounds.Y &&
                   position.Y <= upperBounds.Y;
        }
    }
}