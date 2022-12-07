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
    }
}