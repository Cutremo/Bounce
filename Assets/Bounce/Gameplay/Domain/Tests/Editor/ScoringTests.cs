using System.Collections.Generic;
using System.Linq;
using Bounce.Gameplay.Domain.Runtime;
using FluentAssertions;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;
using static Bounce.Gameplay.Domain.Tests.Builders.AreaBuilder;

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
        public void WinGame()
        {
            var player0 = new Player();
            var area0 = Area().Build();
            var player1 = new Player();
            var area1 = Area().Build();
            var sut = new Game(new Pitch(new Field(Bounds), new Dictionary<Player, Area> {{player0, area0}, {player1, area1}}), 
                new Dictionary<Player, Area> {{player0, area0}, {player1, area1}},
                1);
            sut.Begin();
            
            sut.GivePointTo(player0);

            sut.Playing.Should().BeFalse();
            sut.PointsOf(player0).Should().Be(1);
            sut.PointsOf(player1).Should().Be(0);
        }
    }
}