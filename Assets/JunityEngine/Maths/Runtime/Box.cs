using System;
using System.Linq;

namespace JunityEngine.Maths.Runtime
{
    public class Box
    {
        private readonly Vector2 bottomLeft;
        private readonly Vector2 bottomRight;
        private readonly Vector2 topLeft;
        private readonly Vector2 topRight;

        private Segment BottomSide => new (bottomLeft, bottomRight);
        private Segment TopSide => new (topLeft, topRight);
        private Segment LeftSide => new (bottomLeft, topLeft);
        private Segment RightSide => new (bottomRight, topRight);
        private Segment[] Segments => new[] { BottomSide, TopSide, LeftSide, RightSide };
        public Box(Vector2 bottomLeft, Vector2 topRight) : this(bottomLeft, new Vector2(topRight.X, bottomLeft.Y), new Vector2(bottomLeft.X, topRight.Y), topRight)
        { }

        public Box(Vector2 bottomLeft, Vector2 bottomRight, Vector2 topLeft, Vector2 topRight)
        {
            this.bottomLeft = bottomLeft;
            this.bottomRight = bottomRight;
            this.topLeft = topLeft;
            this.topRight = topRight;
        }

        public Projection Project(Vector2 point)
        {
            return TopSide.NewProject(point);
            return Segments.Where(x => x.CanBeProjected(point))
                .Select(x => x.NewProject(point))
                .OrderBy(x => x.ProjectedDistance)
                .First();
        }
    }
}