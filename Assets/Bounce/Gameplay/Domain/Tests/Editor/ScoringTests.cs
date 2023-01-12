using System.Collections.Generic;
using System.Linq;
using Bounce.Gameplay.Domain.Runtime;
using FluentAssertions;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;
using static Bounce.Gameplay.Domain.Tests.Builders.AreaBuilder;
using static Bounce.Gameplay.Domain.Tests.Builders.BallBuilder;
using static Bounce.Gameplay.Domain.Tests.Builders.GameBuilder;
using static Bounce.Gameplay.Domain.Tests.Builders.PitchBuilder;

namespace Bounce.Gameplay.Domain.Tests.Editor
{
    public class ScoringTests
    {
        Bounds2D Bounds => new Bounds2D(Vector2.NegativeInfinite, Vector2.Infinite);

        [Test]
        public void Score()
        {
            var player0 = new Player();
            var player1 = new Player();
            var players = new List<Player> {player0, player1};
            var score = new Score(players);

            score.GivePointTo(player0);

            score.PointsOf(player0).Should().Be(1);
        }
        
        [Test]
        public void ScoreWhenBallLeavesArea()
        {
            var player0 = new Player();
            var player1 = new Player();
            var pitch = Pitch()
                .AddArea(player0, Area()
                    .WithBounds(new Bounds2D(new Vector2(0, -2), new Vector2(1, -1)))
                    .WithScoringDirection(Vector2.Down)
                    .Build())
                .AddArea(player1, Area()
                    .WithBounds(new Bounds2D(new Vector2(0, 1), new Vector2(1, 2)))
                    .WithScoringDirection(Vector2.Up)
                    .Build())
                .Build();
            var sut = Game()
                .AddPlayer(player0)
                .AddPlayer(player1)
                .WithPitch(pitch)
                .WithTargetScore(1)
                .Build();
            sut.Begin();
            sut.DropBall(Ball().AtTheCenterOf(pitch).WithDiameter(1).Build());
            
            sut.SimulateBall(5f);

            sut.PointsOf(player0).Should().Be(0);
            sut.PointsOf(player1).Should().Be(1);
            sut.Playing.Should().BeFalse();
        }
    }
}