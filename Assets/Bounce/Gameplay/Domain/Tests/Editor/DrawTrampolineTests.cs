﻿using Bounce.Gameplay.Domain.Runtime;
using FluentAssertions;
using JunityEngine;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;

namespace Bounce.Gameplay.Domain.Tests
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

        [Test]
        public void PlayerDrawsTrampoline()
        {
            var sketchbook = new Sketchbook();
            var sut = new Player(sketchbook);
            
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);

            sut.Trampoline.Should().Be(new Trampoline{ Origin = Vector2.Zero, End = Vector2.Up } );
        }

        [Test]
        public void TrampolineRemains()
        {
            var sketchbook = new Sketchbook();
            var sut = new Player(sketchbook);
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);
            
            sut.StopDrawing();

            sut.Trampoline.Should().Be(new Trampoline{ Origin = Vector2.Zero, End = Vector2.Up } );
        }

        [Test]
        public void TrampolineChangesWhenDrawingNew()
        {
            var sketchbook = new Sketchbook();
            var sut = new Player(sketchbook);
            sut.Draw(Vector2.Zero);
            sut.Draw(Vector2.Up);
            sut.StopDrawing();
            
            sut.Draw(new Vector2(3,4));

            sut.Trampoline.Should().Be(new Trampoline{ Origin = new Vector2(3,4), End = new Vector2(3,4) } );
        }
    }
}