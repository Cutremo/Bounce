using FluentAssertions;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;

namespace JunityEngine.Maths.Tests
{
    public class SegmentTests
    {
        [Test]
        public void ProjectPointToSegment()
        {
            var point = Vector2.Half;
            var sut = new Segment(Vector2.Zero, Vector2.Right);
            
            sut.Project(point)
                .Should().Be(Vector2.HalfRight);
        }
        
        [Test]
        public void ProjectPointToSegmentDiagonally()
        {
            var point = Vector2.Half;
            var sut = new Segment(new Vector2(-1, 1), new Vector2(1,-1));
            
            sut.Project(point)
                .Should().Be(Vector2.Zero);
        }
        
        [Test]
        public void SegmentToPoint()
        {
            var point = Vector2.Half;
            var sut = new Segment(Vector2.Zero, Vector2.Right);
            
            sut.To(point)
                .Should().Be(Vector2.HalfUp);
        }

        [Test]
        public void CanBeProjected()
        {
            var point = Vector2.Half;
            var sut = new Segment(Vector2.Zero, Vector2.Right);
            
            sut.CanBeProjected(point)
                .Should().BeTrue();
        }
        
        
        [Test]
        public void CanBeProjectedDiagonally()
        {
            var point = Vector2.Half;
            var sut = new Segment(new Vector2(-1, 1), new Vector2(1, -1));
            
            sut.CanBeProjected(point)
                .Should().BeTrue();
        }
        
        [Test]
        public void CannotBeProjected()
        {
            var point = new Vector2(2, 0);
            var sut = new Segment(Vector2.Zero, Vector2.Right);
            
            sut.CanBeProjected(point)
                .Should().BeFalse();
        }
        //Si esta fuera del segmento devuelve null
    }
}