using System.Linq;
using Bounce.Gameplay.Domain.Runtime;
using FluentAssertions;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;

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

        [Test]
        public void BallWithSizeBounceRight()
        {
            var field = new Field(new Bounds2D(Vector2.Zero, new Vector2(1)));
            var sut = new Ball(Vector2.Half, Vector2.Right, 1);
            field.DropBall(sut);
            
            field.MoveBall(sut, 0.5f);

            sut.Position.Should().Be(Vector2.Half);
        }
        
        
        [Test]
        public void BallWithSizeBounceLeft()
        {
            var field = new Field(new Bounds2D(Vector2.Zero, new Vector2(1)));
            var sut = new Ball(Vector2.Half, Vector2.Left, 1);
            field.DropBall(sut);
            
            field.MoveBall(sut, 0.5f);

            sut.Position.Should().Be(Vector2.Half);
        }
        //Double bounce edge case
    }
}