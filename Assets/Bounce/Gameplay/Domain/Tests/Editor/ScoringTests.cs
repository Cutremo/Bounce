using System.Collections.Generic;
using System.Linq;
using Bounce.Gameplay.Domain.Runtime;
using FluentAssertions;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;

namespace Bounce.Gameplay.Domain.Tests.Editor
{
    public class ScoringTests
    {
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
            var area0 = new Area(new Sketchbook(), Bounds2D.Infinite);
            var player1 = new Player();
            var area1 = new Area(new Sketchbook(), Bounds2D.Infinite);
            var sut = new Game(new Dictionary<Player, Area> {{player0, area0}, {player1, area1}}, 1);
            sut.Begin();
            
            sut.GivePointTo(player0);

            sut.Playing.Should().BeFalse();
            sut.PointsOf(player0).Should().Be(1);
            sut.PointsOf(player1).Should().Be(0);
        }
    }
}