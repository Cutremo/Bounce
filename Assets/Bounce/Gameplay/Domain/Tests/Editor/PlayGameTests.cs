using System.Collections.Generic;
using Bounce.Gameplay.Domain.Runtime;
using FluentAssertions;
using JunityEngine;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;
using static Bounce.Gameplay.Domain.Tests.Builders.AreaBuilder;

namespace Bounce.Gameplay.Domain.Tests.Editor
{
    public class PlayGameTests
    {
        Bounds2D Bounds => Bounds2D.Infinite;
        [Test]
        public void StartsGame()
        {
            var sut = new Game(new Pitch(new Field(Bounds), new Dictionary<Player, Area>()
                {{new Player(), Area().Build()}}), new Dictionary<Player, Area>()
                {{new Player(), Area().Build()}});

            sut.Begin();

            sut.Playing.Should().BeTrue();
        }

        [Test]
        public void NotPlayingByDefault()
        {
            var sut = new Game(new Pitch(new Field(Bounds), new Dictionary<Player, Area>()
                {{new Player(), Area().Build()}}),new Dictionary<Player, Area>()
                {{new Player(), Area().Build()}});

            sut.Playing.Should().BeFalse();
        }

        [Test]
        public void PlayerCanDrawWhenGameStarted()
        {
            var player = new Player();
            var area = Area().Build();
            var sut = new Game(new Pitch(new Field(Bounds), new Dictionary<Player, Area>(new Dictionary<Player, Area>() {{player, area}})), 
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
            var area = Area().Build();
            var sut = new Game(new Pitch(new Field(Bounds), new Dictionary<Player, Area>(new Dictionary<Player, Area>() {{player, area}})),
                new Dictionary<Player, Area> {{player, area}});
            sut.Begin();
            sut.Draw(player, new Vector2(1));
            sut.Draw(player, new Vector2(2));

            sut.StopDrawing(player);

            sut.IsDrawing(player).Should().BeFalse();
            sut.TrampolineOf(player).Should().Be(new Trampoline {Origin = new Vector2(1), End = new Vector2(2)});
        }
    }
}