using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public class BallDropAnimationTests : GameplayFixture
    {
        [Test]
        public void MayNotDrawDuringDropBallAnimation()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-3,0));

            LineRendererOf(player0).positionCount.Should().Be(0);
        }
    }
}