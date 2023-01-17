using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Domain.Tests.Builders;
using FluentAssertions;
using FluentAssertions.Execution;
using JunityEngine;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;
using static Bounce.Gameplay.Domain.Tests.Builders.AreaBuilder;
using static Bounce.Gameplay.Domain.Tests.Builders.BallBuilder;
using static Bounce.Gameplay.Domain.Tests.Builders.PitchBuilder;
using static Bounce.Gameplay.Domain.Tests.Builders.GameBuilder;
using Ball = Bounce.Gameplay.Domain.Runtime.Ball;

namespace Bounce.Gameplay.Domain.Tests.Editor
{
    public class PlayGameTests
    {
        Bounds2D Bounds => Bounds2D.Infinite;
        [Test]
        public void StartsGame()
        {
            var sut = Game().WithPlayers(1).Build();

            sut.Begin();

            sut.Playing.Should().BeTrue();
        }

        [Test]
        public void NotPlayingByDefault()
        {
            var sut = Game().WithPlayers(1).Build();

            sut.Playing.Should().BeFalse();
        }

        [Test]
        public void PlayerCanDrawWhenGameStarted()
        {
            var player = new Player();
            var sut = Game()
                .AddPlayer(player)
                .WithPitch(Pitch().AddArea(player, Area().Build()).Build())
                .Build();
            
            sut.Begin();

            sut.Draw(player, new Vector2(5));

            using var _ = new AssertionScope();
            sut.TrampolineOf(player).Should().Be(new Trampoline {Origin = new Vector2(5), End = new Vector2(5)});
            sut.IsDrawing(player).Should().BeTrue();
        }

        [Test]
        public void PlayerStopsDrawing()
        {
            var player = new Player();
            var sut = Game()
                .AddPlayer(player)
                .WithPitch(Pitch().AddArea(player, Area().Build()).Build())
                .Build();
            
            sut.Begin();
            sut.Draw(player, new Vector2(1));
            sut.Draw(player, new Vector2(2));

            sut.StopDrawing(player);

            using var _ = new AssertionScope();
            sut.IsDrawing(player).Should().BeFalse();
            sut.TrampolineOf(player).Should().Be(new Trampoline {Origin = new Vector2(1), End = new Vector2(2)});
        }

        [Test, Ignore("Unsure about this feature")]
        public void ClearingPitchDeletesTrampolines()
        {
            var player = new Player();
            var area = Area()
                .WithBounds(new Bounds2D(Vector2.MinusOne, new Vector2(5)))
                .WithScoringDirection(Vector2.Down)
                .Build();
            var pitch = Pitch().AddArea(player, area).Build();
            var sut = Game()
                .AddPlayer(player)
                .WithPitch(pitch)
                .Build();
            sut.Begin();
            sut.DropBall(Ball().WithDiameter(1).AtTheCenterOf(pitch).Build());
            sut.Draw(player, Vector2.Down);
            sut.Draw(player, Vector2.Up);
            sut.StopDrawing(player);
            
            sut.SimulateBall(5f);

            using var _ = new AssertionScope();
            sut.TrampolineOf(player).Should().Be(Trampoline.Null);
        }
        
        [Test, Ignore("Unsure about this feature")]
        public void ClearingPitchRemovesDrawings()
        {
            var player = new Player();
            var area = Area()
                .WithBounds(new Bounds2D(Vector2.MinusOne, new Vector2(5)))
                .WithScoringDirection(Vector2.Down)
                .Build();
            var pitch = Pitch().AddArea(player, area).Build();
            var sut = Game()
                .AddPlayer(player)
                .WithPitch(pitch)
                .Build();
            sut.Begin();
            sut.DropBall(Ball().WithDiameter(1).AtTheCenterOf(pitch).Build());
            sut.Draw(player, Vector2.Down);
            sut.Draw(player, Vector2.Up);
            
            sut.SimulateBall(5f);

            using var _ = new AssertionScope();
            sut.IsDrawing(player).Should().BeFalse();
            sut.TrampolineOf(player).Should().Be(Trampoline.Null);
        }
        
        [Test]
        public void RemoveBall()
        {
            var player = new Player();
            var area = Area()
                .WithBounds(new Bounds2D(Vector2.MinusOne, new Vector2(5)))
                .WithScoringDirection(Vector2.Down)
                .Build();
            var pitch = Pitch().AddArea(player, area).Build();
            var sut = Game()
                .AddPlayer(player)
                .WithPitch(pitch)
                .Build();
            sut.Begin();
            sut.DropBall(Ball().WithDiameter(1).AtTheCenterOf(pitch).Build());

            sut.RemoveBall();

            using var _ = new AssertionScope();
            sut.Ball.Should().Be(Ball.Null);
        }
    }
}