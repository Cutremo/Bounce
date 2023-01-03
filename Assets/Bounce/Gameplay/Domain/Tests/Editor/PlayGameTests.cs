using System.Collections.Generic;
using Bounce.Gameplay.Domain.Runtime;
using FluentAssertions;
using JunityEngine;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;

namespace Bounce.Gameplay.Domain.Tests.Editor
{
    public class PlayGameTests
    {
        [Test]
        public void StartsGame()
        {
            var sut = new Game(new Pitch(new Bounds2D()), new Dictionary<Player, Area>()
                {{new Player(), new Area(new Sketchbook(), Bounds2D.Infinite)}});

            sut.Begin();

            sut.Playing.Should().BeTrue();
        }

        [Test]
        public void NotPlayingByDefault()
        {
            var sut = new Game(new Pitch(new Bounds2D()),new Dictionary<Player, Area>()
                {{new Player(), new Area(new Sketchbook(), Bounds2D.Infinite)}});

            sut.Playing.Should().BeFalse();
        }

        [Test]
        public void PlayerCanDrawWhenGameStarted()
        {
            var player = new Player();
            var area = new Area(new Sketchbook(), Bounds2D.Infinite);
            var sut = new Game(new Pitch(new Bounds2D()), 
                new Dictionary<Player, Area>(new Dictionary<Player, Area>() {{player, area}}));
            sut.Begin();

            sut.Draw(player, new Vector2(5));

            sut.TrampolineOf(player).Should().Be(new Trampoline {Origin = new Vector2(5), End = new Vector2(5)});
            sut.IsDrawing(player).Should().BeTrue();
        }

        [Test]
        public void PlayerStopsDrawing()
        {
            var player = new Player();
            var area = new Area(new Sketchbook(), Bounds2D.Infinite);
            var sut = new Game(new Pitch(new Bounds2D()),
                new Dictionary<Player, Area> {{player, area}});
            sut.Begin();
            sut.Draw(player, new Vector2(5));

            sut.StopDrawing(player);

            sut.IsDrawing(player).Should().BeFalse();
            sut.TrampolineOf(player).Should().Be(new Trampoline {Origin = new Vector2(5), End = new Vector2(5)});
        }
    }
}