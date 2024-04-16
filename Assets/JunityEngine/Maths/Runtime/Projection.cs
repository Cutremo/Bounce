namespace JunityEngine.Maths.Runtime
{
    public struct Projection
    {
        private readonly Vector2 origin;
        private readonly Vector2 projectedPoint;
        public Segment OriginToProjection => new(origin, projectedPoint);
        public Segment ProjectionToOrigin => OriginToProjection.Reverse();
        public Vector2 NormalDirection => ProjectionToOrigin.AToB.Normalize;

        public float ProjectedDistance => OriginToProjection.Magnitude;
        public Projection(Vector2 origin, Vector2 projectedPoint)
        {
            this.origin = origin;
            this.projectedPoint = projectedPoint;
        }
    }
}