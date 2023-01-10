using RGV.DesignByContract.Runtime;

namespace JunityEngine.Maths.Runtime
{
    public readonly struct Segment
    {
        public Vector2 A { get; init; }
        public Vector2 B { get; init; }

        public Segment(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }

        public Vector2 AToB => B - A;
        public Vector2 BToA => A - B;

        public Segment Pararel0(float distance)
        {
            return new Segment(A + AToB.NormalDirection0 * distance, B + AToB.NormalDirection0 * distance);
        }
        
        public Segment Pararel1(float distance)
        {
            return new Segment(A + AToB.NormalDirection1 * distance, B + AToB.NormalDirection1 * distance);
        }

        // t == collision scalar in other.
        // u == collision scalar in this
        // other + t * other.AToB == A + u * AToB.
        public Vector2 CollisionTo(Segment other, bool includeOrigin = false)
        {
            var t = (A - other.A).Cross(AToB) / other.AToB.Cross(AToB);
            var u = (other.A - A).Cross(other.AToB) / AToB.Cross(other.AToB);

            if(t is >= 0 and <= 1 && ((includeOrigin && u >= 0) || u > 0) && u <= 1)
                return A + u * AToB;
            return Vector2.Null;
        }

        public Vector2 Reflect(Vector2 vector)
        {
            return vector - 2 * vector.Dot(AToB.NormalDirection0) * AToB.NormalDirection0;
        }

        public Segment ExtendFromBothEnds(float magnitude)
        {
            return new Segment(A + BToA.Normalize * magnitude, B + AToB.Normalize * magnitude);
        }

        public bool CanBeProjected(Vector2 point)
        {
            float recArea = AToB.Dot(AToB);
            float val = AToB.Dot(point);
            return (val >= 0 && val <= recArea);
        }
        public Vector2 Project(Vector2 point)
        {
            // Contract.Require(CanBeProjected(point)).True();
            return point.Dot(AToB) / AToB.Dot(AToB) * AToB;
        }

        public Vector2 To(Vector2 point)
        {
            var pointOnLine = Project(point);
            return AToB.NormalDirection0 * point.DistanceTo(pointOnLine);
        }
    }
}