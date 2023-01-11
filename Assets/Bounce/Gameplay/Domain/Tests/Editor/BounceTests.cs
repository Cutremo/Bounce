using System.Collections.Generic;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Domain.Tests.Builders;
using FluentAssertions;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;
using static Bounce.Gameplay.Domain.Tests.Builders.AreaBuilder;

namespace Bounce.Gameplay.Domain.Tests.Editor
{
    public class BounceTests
    {
        [Test]
        public void Bounces()
        {
            var player = new Player();
            var area = Area().Build();
            var sut = PitchBuilder.Pitch()
                .AddArea(player, area)
                .Build();
            var ball = new Ball(Vector2.One, Vector2.Down);
            sut.DropBall(ball);
            sut.Draw(player, Vector2.Half);
            sut.Draw(player, new Vector2(1.5f, 0.5f));

            sut.SimulateBall(1f);

            ball.Position.Should().Be(Vector2.One);
        }
        
        [Test]
        public void ChangesDirectionAfterBounce()
        {
            var player = new Player();
            var area = Area().Build();
            var sut = PitchBuilder.Pitch()
                .AddArea(player, area)
                .Build();
            var ball = new Ball(Vector2.One, Vector2.Down);
            sut.DropBall(ball);
            sut.Draw(player, Vector2.Half);
            sut.Draw(player, new Vector2(1.5f, 0.5f));
            sut.SimulateBall(1f);

            sut.SimulateBall(1f);

            ball.Position.Should().Be(new Vector2(1, 2));
        }

        [Test]
        public void DiagonalDirection()
        {
            var player = new Player();
            var area = Area().Build();
            var sut = PitchBuilder.Pitch()
                .AddArea(player, area)
                .Build();
            var ball = new Ball(new Vector2(1, 1.5f), Vector2.Down);
            sut.DropBall(ball);
            sut.Draw(player, new Vector2(1.5f, 1.5f));
            sut.Draw(player, Vector2.Half);
            
            sut.SimulateBall(1f);
            
            ball.Position.Should().Be(new Vector2(0.5f, 1f));
        }
        
        [Test]
        public void BallWithRadius()
        {
            var player = new Player();
            var area = Area().Build();
            var sut = PitchBuilder.Pitch()
                .AddArea(player, area)
                .Build();
            
            var ball = new Ball(new Vector2(1, 1.5f), Vector2.Down, 1);
            sut.DropBall(ball);
            sut.Draw(player, Vector2.Half);
            sut.Draw(player, new Vector2(1.5f, 0.5f));

            sut.SimulateBall(1f);

            ball.Position.Should().Be(new Vector2(1, 1.5f));
        }

        [Test]
        public void DrawingOnTheOppositeDirectionDoesNotChangeBounce()
        {
            var player = new Player();
            var area = Area().Build();
            var sut = PitchBuilder.Pitch()
                .AddArea(player, area)
                .Build();
            
            var ball = new Ball(new Vector2(1, 1.5f), Vector2.Down);
            sut.DropBall(ball);
            sut.Draw(player, Vector2.Half);
            sut.Draw(player, new Vector2(1.5f, 1.5f));

            sut.SimulateBall(1f);
            
            ball.Position.Should().Be(new Vector2(0.5f, 1f));
        }

        [Test]
        public void SpeedIncreases()
        {
            var player = new Player();
            var area = Area()
                .WithSpeedBoost(1)
                .Build();
            var sut = PitchBuilder.Pitch()
                .AddArea(player, area)
                .Build();
            
            var ball = new Ball(Vector2.One, Vector2.Down);
            sut.DropBall(ball);
            sut.Draw(player, Vector2.Half);
            sut.Draw(player, new Vector2(1.5f, 0.5f));

            sut.SimulateBall(1f);

            ball.Speed.Should().Be(2);
        }
    }
}