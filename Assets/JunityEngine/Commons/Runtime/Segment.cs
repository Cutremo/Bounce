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
    }
}