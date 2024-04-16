using FluentAssertions;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;

namespace JunityEngine.Maths.Tests
{
    public class BoxTests
    {
        [Test]
        public void METHOD()
        {
            var sut = new Box(Vector2.Zero + Vector2.One, Vector2.Right + Vector2.One, Vector2.Up + Vector2.One, Vector2.One + Vector2.One);

            sut.Project(new Vector2(0.5f, 3f))
                .Should().Be(new Projection(new Vector2(0.5f, 3f), new Vector2(0.5f, 2f)));
        }
        
        [Test]
        public void METHODs()
        {
            var sut = new Box(Vector2.Zero, Vector2.One);

            sut.Project(new Vector2(2f, 2f))
                .Should().Be(new Projection(new Vector2(0.5f, 1f), new Vector2(0, 1f)));
        }
        
        //Ninguna proyeccion disponible
    }
}