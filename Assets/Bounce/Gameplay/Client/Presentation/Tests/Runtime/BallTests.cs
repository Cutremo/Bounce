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
    public class BallTests : InPointFixture
    {
        [Test]
        public void BallExistsAtBeginningOfPoint()
        {
            Ball.Should().NotBeNull();
        }
        
        [UnityTest]
        public IEnumerator BallMoves()
        {
            var originalPosition = Ball.transform.position;
            
            yield return AssertThatHappensInTime(() => Ball.transform.position != originalPosition, 1f);
        }
    }
}
