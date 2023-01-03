using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Tests.Editor
{
    public class BallTests
    {
        [Test]
        public void DropBall()
        {
            var sut = new Field(new Bounds2D(Vector2.Zero, Vector2.One));
            
            sut.DropBall(new Ball(Vector2.Zero));

            sut.Balls.Count().Should().Be(1);
        }

        [Test]
        public void MoveBall()
        {
            var field = new Field(new Bounds2D(Vector2.Zero, new Vector2(5)));
            var sut = new Ball(Vector2.Zero, Vector2.Right);
            field.DropBall(sut);
            
            field.MoveBall(sut, 2f);

            sut.Position.Should().Be(new Vector2(2, 0));
        }

        [Test]
        public void BounceOnRightEdge()
        {
            var field = new Field(new Bounds2D(Vector2.Zero, new Vector2(2)));
            var sut = new Ball(Vector2.Zero, Vector2.Right);
            field.DropBall(sut);
            
            field.MoveBall(sut, 3f);

            sut.Position.Should().Be(new Vector2(1, 0));
        }
        
        [Test]
        public void BounceOnLeftEdge()
        {
            var field = new Field(new Bounds2D(Vector2.Zero, new Vector2(2)));
            var sut = new Ball(Vector2.Right, Vector2.Left);
            field.DropBall(sut);
            
            field.MoveBall(sut, 3f);

            sut.Position.Should().Be(new Vector2(2, 0));
        }

        [Test]
        public void DiagonalMovement()
        {
            var field = new Field(new Bounds2D(Vector2.Zero, new Vector2(2)));
            var sut = new Ball(Vector2.Zero, Vector2.One.Normalize);
            field.DropBall(sut);
            
            field.MoveBall(sut, 1);

            sut.Position.Should().Be(new Vector2(1, Vector2.One.Normalize));
        }
        
        //Double bounce edge case
    }
    
    public class Field
    {
        readonly List<Ball> balls = new();
        readonly Bounds2D bounds;
        
        public Field(Bounds2D bounds)
        {
            this.bounds = bounds;
        }

        public void MoveBall(Ball ball, float amount)
        {
            Contract.Require(balls.Contains(ball)).True();
            var targetPosition = ball.Position + ball.Orientation * amount;
            
            if(bounds.OnRight(targetPosition))
                targetPosition = new Vector2(bounds.RightEdge, 0) - targetPosition + new Vector2(bounds.RightEdge, 0);
            else if(bounds.OnLeft(targetPosition))
                targetPosition = new Vector2(bounds.LeftEdge, 0) - targetPosition + new Vector2(bounds.LeftEdge, 0);
            
            ball.Position = targetPosition;
        }
        
        public IEnumerable<Ball> Balls => balls;

        public void DropBall(Ball ball)
        {
            Contract.Require(bounds.Contains(ball.Position)).True();
            balls.Add(ball);
        }
    }

    public class Ball
    {
        public Vector2 Position { get; set; }

        Vector2 orientation;

        public Vector2 Orientation
        {
            get => orientation;
            set
            {
                Contract.Require(value.Normalized).True();
                orientation = value;
            }
        }

        public Ball(Vector2 position)
        {
            Position = position;
        }
        
        public Ball(Vector2 position, Vector2 orientation) : this(position)
        {
            Orientation = orientation;
        }
    }
}