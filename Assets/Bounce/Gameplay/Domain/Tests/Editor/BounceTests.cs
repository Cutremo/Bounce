using System.Collections.Generic;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Domain.Tests.Builders;
using FluentAssertions;
using FluentAssertions.Execution;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;
using static Bounce.Gameplay.Domain.Tests.Builders.AreaBuilder;

namespace Bounce.Gameplay.Domain.Tests.Editor
{
    public class BounceTests
    {
        Player SomePlayer => new("SomePlayer");
        [Test]
        public void Bounces()
        {
            var sut = PitchBuilder.Pitch()
                .AddArea(SomePlayer)
                .Build();
            sut.DropBall(new Ball(Vector2.One, Vector2.Down));
            sut.Draw(SomePlayer, Vector2.Half);
            sut.Draw(SomePlayer, new Vector2(1.5f, 0.5f));

            sut.SimulateBall(1f);

            sut.Ball.Position.Should().Be(Vector2.One);
        }
        
        [Test]
        public void ChangesDirectionAfterBounce()
        {
            var sut = PitchBuilder.Pitch()
                .AddArea(SomePlayer)
                .Build();
            sut.DropBall(new Ball(Vector2.One, Vector2.Down));
            sut.Draw(SomePlayer, Vector2.Half);
            sut.Draw(SomePlayer, new Vector2(1.5f, 0.5f));
            sut.SimulateBall(1f);

            sut.SimulateBall(1f);

            sut.Ball.Position.Should().Be(new Vector2(1, 2));
        }

        [Test]
        public void DiagonalDirection()
        {
            var sut = PitchBuilder.Pitch()
                .AddArea(SomePlayer)
                .Build();
            sut.DropBall(new Ball(new Vector2(1, 1.5f), Vector2.Down));
            sut.Draw(SomePlayer, new Vector2(1.5f, 1.5f));
            sut.Draw(SomePlayer, Vector2.Half);
            
            sut.SimulateBall(1f);
            
            sut.Ball.Position.Should().Be(new Vector2(0.5f, 1f));
        }
        
        [Test]
        public void BallWithRadius()
        {
            var sut = PitchBuilder.Pitch()
                .AddArea(SomePlayer)
                .Build();
            sut.DropBall(new Ball(new Vector2(1, 1.5f), Vector2.Down, 1));
            sut.Draw(SomePlayer, Vector2.Half);
            sut.Draw(SomePlayer, new Vector2(1.5f, 0.5f));

            sut.SimulateBall(1f);

            sut.Ball.Position.Should().Be(new Vector2(1, 1.5f));
        }

        [Test]
        public void DrawingOnTheOppositeDirectionDoesNotChangeBounce()
        {
            var sut = PitchBuilder.Pitch()
                .AddArea(SomePlayer)
                .Build();
            sut.DropBall(new Ball(new Vector2(1, 1.5f), Vector2.Down));
            sut.Draw(SomePlayer, Vector2.Half);
            sut.Draw(SomePlayer, new Vector2(1.5f, 1.5f));

            sut.SimulateBall(1f);
            
            sut.Ball.Position.Should().Be(new Vector2(0.5f, 1f));
        }

        [Test]
        public void SpeedIncreases()
        {
            var sut = PitchBuilder.Pitch()
                .AddArea(SomePlayer, Area().WithSpeedBoost(1).Build())
                .Build();

            sut.DropBall(new Ball(Vector2.One, Vector2.Down));
            sut.Draw(SomePlayer, Vector2.Half);
            sut.Draw(SomePlayer, new Vector2(1.5f, 0.5f));

            sut.SimulateBall(1f);

            sut.Ball.Speed.Should().Be(2);
        }
        
        [Test]
        public void DrawingIsRemovedAfterBounce()
        {
            var sut = PitchBuilder.Pitch()
                .AddArea(SomePlayer)
                .Build();
            sut.DropBall(new Ball(Vector2.One, Vector2.Down));
            sut.Draw(SomePlayer, Vector2.Half);
            sut.Draw(SomePlayer, new Vector2(1.5f, 0.5f));

            sut.SimulateBall(1f);

            sut.TrampolineOf(SomePlayer).Should().Be(Trampoline.Null);
        }
        
        [Test]
        public void TrampolineIsRemovedAfterBounce()
        {
            var sut = PitchBuilder.Pitch()
                .AddArea(SomePlayer)
                .Build();
            sut.DropBall(new Ball(Vector2.One, Vector2.Down));
            sut.Draw(SomePlayer, Vector2.Half);
            sut.Draw(SomePlayer, new Vector2(1.5f, 0.5f));
            sut.StopDrawing(SomePlayer);
            
            sut.SimulateBall(1f);

            sut.TrampolineOf(SomePlayer).Should().Be(Trampoline.Null);
        }
    }
}