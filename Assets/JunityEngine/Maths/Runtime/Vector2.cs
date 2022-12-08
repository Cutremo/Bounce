using System;

namespace JunityEngine.Maths.Runtime
{
    public struct Vector2 
    {
        public float X { get; }
        public float Y { get; }
        public float Size => (float) Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

        public static Vector2 Zero => new(0, 0);
        public static Vector2 One => new(1, 1);
        public static Vector2 MinusOne => new(-1, -1);
        public static Vector2 Half => new(0.5f, 0.5f);
        public static Vector2 Up => new(0, 1);
        public static Vector2 Down => new(0, -1);
        public static Vector2 Right => new(1, 0);
        public static Vector2 Left => new(-1, 0);        
        public static Vector2 NegativeInfinite => new(float.NegativeInfinity, float.NegativeInfinity);
        public static Vector2 Infinite => new(float.PositiveInfinity, float.PositiveInfinity);

        public Vector2(float componentsValue)
        {
            X = Y = componentsValue;
        }
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public static Vector2 operator + (Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);

        public static Vector2 operator - (Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator * (Vector2 v, int t) => new(v.X * t, v.Y * t);
        public Vector2 To(Vector2 other) => other - this;
        public Vector2 Normalized => new(X / Size, Y / Size);
        public Vector2 WithSymmetryOnYAxis => new(-X, Y);
        public Vector2 WithSymmetryOnXAxis => new(X, -Y);
        public Vector2 Reverse => new(-X, -Y);

        public override string ToString() => "(" + X + ", " + Y +")";
    }
}
