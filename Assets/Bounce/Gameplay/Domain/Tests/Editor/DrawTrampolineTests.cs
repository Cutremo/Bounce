using Bounce.Gameplay.Domain.Runtime;
using FluentAssertions;
using JunityEngine;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;

namespace Bounce.Gameplay.Domain.Tests.Editor
{
    public class DrawTrampolineTests
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

            sut.Result.Should().BeEquivalentTo(new Trampoline {Origin = Vector2.Zero, End = Vector2.Up});
        }

        [Test]
        public void ClampToMaxLength()
        {
            var sut = new Sketchbook {MaxTrampolineLength = 1};
            sut.Draw(Vector2.Zero);

            sut.Draw(new Vector2(2, 0));

            sut.Result.Should().Be(new Trampoline {Origin = new Vector2(1, 0), End = new Vector2(2, 0)});
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

        [Test]
        public void PlayerDrawsTrampoline()
        {
            var sketchbook = new Sketchbook();
            var sut = new Area(sketchbook, Bounds2D.Infinite);

            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);

            sut.Trampoline.Should().Be(new Trampoline {Origin = Vector2.Zero, End = Vector2.Up});
        }

        [Test]
        public void TrampolineRemains()
        {
            var sketchbook = new Sketchbook();
            var sut = new Area(sketchbook, Bounds2D.Infinite);
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);

            sut.StopDrawing();

            sut.Trampoline.Should().Be(new Trampoline {Origin = Vector2.Zero, End = Vector2.Up});
        }

        [Test]
        public void TrampolineChangesWhenDrawingNew()
        {
            var sketchbook = new Sketchbook();
            var sut = new Area(sketchbook, Bounds2D.Infinite);
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);
            sut.StopDrawing();

            sut.Draw(new Vector2(3, 4));

            sut.Trampoline.Should().Be(new Trampoline {Origin = new Vector2(3, 4), End = new Vector2(3, 4)});
        }

        [Test]
        public void MinTrampolineSizeWhenEndingDraw()
        {
            var sketchbook = new Sketchbook();
            var sut = new Area(sketchbook, Bounds2D.Infinite) {MinTrampolineSize = 1};
            sut.Draw(new Vector2(0.25f, 0));
            sut.Draw(new Vector2(0.75f, 0));           
            
            sut.StopDrawing();

            sut.Trampoline.Should().Be(new Trampoline {Origin = Vector2.Zero, End = Vector2.Right});
        }
        
        [Test]
        public void MinTrampolineSizeDoesNotApplyBeforeEndingDraw()
        {
            var sketchbook = new Sketchbook();
            var sut = new Area(sketchbook, Bounds2D.Infinite) {MinTrampolineSize = 1};
            
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.HalfRight);
            
            sut.Trampoline.Should().Be(new Trampoline {Origin = Vector2.Zero, End = Vector2.HalfRight});
        }

        [Test]
        public void MinTrampolineSizeWhenOriginIsAtBorder()
        {
            var sketchbook = new Sketchbook();
            var sut = new Area(sketchbook, Bounds2D.Infinite) {MinTrampolineSize = 1};
            sut.Draw(new Vector2(0.25f, 0));
            sut.Draw(new Vector2(0.75f, 0));           
            
            sut.StopDrawing();

            sut.Trampoline.Should().Be(new Trampoline {Origin = Vector2.Zero, End = Vector2.Right});
        }

        [Test]
        public void BeginDrawFromOutsideBoundsDoesNotDraw()
        {
            var sketchbook = new Sketchbook();
            var sut = new Area(sketchbook, Bounds2D.One);

            sut.Draw(new Vector2(2));

            sut.Trampoline.Should().Be(Trampoline.Null);
        }
        
        
        [Test]
        public void DrawingOutsideClamps()
        {
            var sketchbook = new Sketchbook();
            var sut = new Area(sketchbook, Bounds2D.One);
            sut.Draw(Vector2.Zero);

            sut.Draw(new Vector2(2));

            sut.Trampoline.Should().Be(new Trampoline { Origin = Vector2.Zero, End = Vector2.One});
        }
        
        //Trampolin tamaño minimo borde cercano al origen
        //Trampolin tamaño minimo borde cercano al final.
        //Trampolin tamaño minimo 2 bordes
    }
}
    