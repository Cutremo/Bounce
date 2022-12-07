using Bounce.Gameplay.Runtime.Domain;
using Bounce.Runtime.Domain;
using FluentAssertions;
using JunityEngine;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;

namespace Bounce.Bounce.Tests
{
    public class DrawLineTests
    {
        [Test]
        public void BeginDraw()
        {
            var sut = new Sketchbook();

            sut.Draw(Vector2.Zero);

            sut.Drawing.Should().BeTrue();
        }

        [Test]
        public void Draw()
        {
            var sut = new Sketchbook();
            sut.Draw(Vector2.Zero);
            
            sut.Draw(Vector2.Up);

            sut.Result.Should().BeEquivalentTo(new Trampoline { Origin = Vector2.Zero, End = Vector2.Up });
        }

        [Test]
        public void ClampToMaxLength()
        {
            var sut = new Sketchbook { MaxTrampolineLength = 1};
            sut.Draw(Vector2.Zero);
            
            sut.Draw(new Vector2(2, 0 ));

            sut.Result.Should().Be(new Trampoline { Origin = new Vector2(1, 0), End = new Vector2(2, 0) });
        }

        [Test]
        public void EndDraw()
        {
            var sut = new Sketchbook();
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);

            sut.StopDrawing();
            
            sut.Result.Should().BeAssignableTo<INull>();
        }
    }
}