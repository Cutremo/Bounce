using FluentAssertions;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;

namespace JunityEngine.Commons.Tests
{
    public class Bounds2DTest
    {
        [Test]
        public void LowerEdge()
        {
            var sut = new Bounds2D(Vector2.Zero, Vector2.One);
            sut.Contains(Vector2.Zero)
                .Should().BeTrue();
        }
        
        [Test]
        public void UpperEdge()
        {
            var sut = new Bounds2D(Vector2.Zero, Vector2.One);
            sut.Contains(Vector2.One)
                .Should().BeTrue();
        }

        [Test]
        public void Middle()
        {
            var sut = new Bounds2D(Vector2.Zero, Vector2.One);
            sut.Contains(Vector2.Half)
                .Should().BeTrue();
        }
        
        [Test]
        public void Outside()
        {
            var sut = new Bounds2D(Vector2.Zero, Vector2.One);
            sut.Contains(Vector2.MinusOne)
                .Should().BeFalse();
        }

        [Test]
        public void IntersectionRightEdge()
        {
            var sut = new Bounds2D(Vector2.Zero, Vector2.One);
            
            sut.ClampWithRaycast(Vector2.Half, new Vector2(1.5f, 0.5f))
                .Should().Be(new Vector2(1f, 0.5f));
        }
        
        [Test]
        public void IntersectionOnOrigin()
        {
            var sut = new Bounds2D(Vector2.Zero, Vector2.One);
            
            sut.ClampWithRaycast(Vector2.Zero, Vector2.Half)
                .Should().Be(Vector2.Half);
        }
        
        [Test]
        public void IntersectionOnExactTarget()
        {
            var sut = new Bounds2D(Vector2.Zero, Vector2.One);
            
            sut.ClampWithRaycast(Vector2.Half, Vector2.One)
                .Should().Be(Vector2.One);
        }
        [Test]
        public void NoIntersection()
        {
            var sut = new Bounds2D(Vector2.Zero, Vector2.One);
            
            sut.ClampWithRaycast(Vector2.Half, new Vector2(0.75f))
                .Should().Be(new Vector2(0.75f));
        }
        
        [Test]
        public void IntersectionLeftEdge()
        {
            var sut = new Bounds2D(Vector2.Zero, Vector2.One);
            
            sut.ClampWithRaycast(Vector2.Half, new Vector2(-0.5f, 0.5f))
                .Should().Be(Vector2.HalfUp);
        }

        [Test]
        public void Clamp()
        {
            var sut = new Bounds2D(Vector2.Zero, Vector2.One);

            sut.Clamp(new Vector2(0.5f, 5))
                .Should().Be(new Vector2(0.5f, 1));
        }
    }
}