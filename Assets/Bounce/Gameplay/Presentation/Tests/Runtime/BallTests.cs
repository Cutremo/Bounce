using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public class BallTests : InSceneFixture
    {
        [Test]
        public async Task BallExists()
        {
            await Task.Yield();

            Object.FindObjectsOfType<BallView>()
                .Length.Should().Be(1);
        }
        
        [Test]
        public async Task BallMoves()
        {
            var originalPosition = Object.FindObjectOfType<BallView>().transform.position;
            
            await Task.Delay(3000);

            Object.FindObjectOfType<BallView>().transform.position.Should().NotBe(originalPosition);
        }
    }
}
