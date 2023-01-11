using System;
using RGV.DesignByContract.Runtime;

namespace JunityEngine.Maths.Runtime
{
    public readonly struct Bounds2D
    {
        const float Tolerance = 0.0001f;

        //Divide by 2 to avoid center at infinite position.
        public static Bounds2D Infinite => new(Vector2.MinValue / 2, Vector2.MaxValue / 2);

        public Vector2 Size => new Vector2(SizeX, SizeY);
        
        public float SizeX => upperBounds.X - lowerBounds.X;
        public float SizeY => upperBounds.Y - lowerBounds.Y;
        public float LeftEdgeX => lowerBounds.X;
        public float RightEdgeX => upperBounds.X;
        public float BottomEdgeY => lowerBounds.Y;
        public float TopEdgeY => upperBounds.Y;
        public Vector2 TopRightCorner => new Vector2(upperBounds.X, upperBounds.Y);
        public Vector2 TopLeftCorner => new Vector2(lowerBounds.X, upperBounds.Y);
        public Vector2 BottomRightCorner => new Vector2(upperBounds.X, lowerBounds.Y);
        public Vector2 BottomLeftCorner => new Vector2(lowerBounds.X, lowerBounds.Y);

        #region Edges
        public Segment RightEdge => new(TopRightCorner, BottomRightCorner);
        public Segment TopEdge => new(TopRightCorner, TopLeftCorner);
        public Segment LeftEdge => new(BottomLeftCorner, TopLeftCorner);
        public Segment BottomEdge => new(BottomRightCorner, BottomLeftCorner);
        #endregion


        public Vector2 Center => lowerBounds + (upperBounds - lowerBounds) / 2;
        public static Bounds2D One => new(Vector2.Zero, Vector2.One);

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

        public bool OnLeft(Vector2 position) => position.X < lowerBounds.X;
        public bool OnRight(Vector2 position) => position.X > upperBounds.X;
        public bool OnTop(Vector2 position) => position.Y > upperBounds.Y;
        public bool OnBottom(Vector2 position) => position.Y < lowerBounds.Y;
        
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

        public Vector2 ClampWithRaycast(Vector2 origin, Vector2 target)
        {
            var segment = new Segment(origin, target);
            if(segment.CollisionTo(RightEdge) != Vector2.Null)
                return segment.CollisionTo(RightEdge);
            
            if(segment.CollisionTo(LeftEdge) != Vector2.Null)
                return segment.CollisionTo(LeftEdge);
            
            if(segment.CollisionTo(TopEdge) != Vector2.Null)
                return segment.CollisionTo(TopEdge);
            
            if(segment.CollisionTo(BottomEdge) != Vector2.Null)
                return segment.CollisionTo(BottomEdge);

            return target;
        }

        public bool IsOutsideOnDirection(Vector2 position, Vector2 direction)
        {
            if(direction == Vector2.Right)
                return OnRight(position);
            if(direction == Vector2.Left)
                return OnLeft(position);
            if(direction == Vector2.Up)
                return OnTop(position);
            if(direction == Vector2.Down)
                return OnBottom(position);

            throw new NotImplementedException("Does not support diagonals");
        }
    }
}