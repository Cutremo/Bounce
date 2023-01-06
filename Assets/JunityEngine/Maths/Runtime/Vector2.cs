using System;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace JunityEngine.Maths.Runtime
{
    public readonly struct Vector2
    {
        const float Tolerance = 0.001f;
        public float X { get; }
        public float Y { get; }
        
        public float Magnitude => (float) Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

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
        public static Vector2 HalfRight => new(0.5f, 0);
        public static Vector2 HalfUp => new(0, 0.5f);
        public static Vector2 Null => new(float.NaN, float.NaN);
        public Vector2 NormalDirection0 => new Vector2(-Y, X).Normalize;
        public Vector2 NormalDirection1 => new Vector2(Y, -X).Normalize;
        public Vector2(float componentsValue)
        {
            X = Y = componentsValue;
        }
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public Vector2(float size, Vector2 direction)
        {
            Require(direction.Normalized).True();
            
            X = direction.X * size;
            Y = direction.Y * size;
        }

        
        public static Vector2 operator + (Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);

        public static Vector2 operator - (Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator * (Vector2 v, int t) => new(v.X * t, v.Y * t);
        public static Vector2 operator * (Vector2 v, float t) => new(v.X * t, v.Y * t);
        public static Vector2 operator / (Vector2 v, int d) => new(v.X / d, v.Y / d);
        public static Vector2 operator / (Vector2 v, float d) => new(v.X / d, v.Y / d);
        public static Vector2 operator * (int t, Vector2 v) => new(v.X * t, v.Y * t);
        public static Vector2 operator * (float t, Vector2 v) => new(v.X * t, v.Y * t);
        public Vector2 To(Vector2 other) => other - this;
        public bool Normalized => Equals(this, Normalize);
        public Vector2 Normalize => new(X / Magnitude, Y / Magnitude);
        public Vector2 SymmetricOnYAxis => new(-X, Y);
        public Vector2 SymmetricOnXAxis => new(X, -Y);
        public Vector2 Reverse => new(-X, -Y);

        public override string ToString() => "(" + X + ", " + Y +")";

        public bool Equals(Vector2 other)
        {
            if(float.IsNaN(X) && float.IsNaN(Y) && float.IsNaN(other.X) && float.IsNaN(other.Y))
                return true;
            
            return Math.Abs(X - other.X) <= Tolerance && Math.Abs(Y - other.Y) <= Tolerance;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !left.Equals(right);
        }

        public readonly Vector2 WithX(float newX)
        {
            return new Vector2(newX, Y);
        }
        public readonly Vector2 WithY(float newY)
        {
            return new Vector2(X, newY);
        }

        public float Cross(Vector2 other)
        {
            return X * other.Y - Y * other.X;
        }

        public float Dot(Vector2 other)
        {
            return X * other.X + Y * other.Y;
        }
    }
}
