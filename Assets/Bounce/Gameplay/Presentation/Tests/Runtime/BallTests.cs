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
    public class BallTests : MonoBehaviour
    {
        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            SceneManager.LoadScene("MainScene");
            yield return null;
            yield return null;
        }
        
        [Test]
        public void BallExists()
        {
            FindObjectsOfType<BallView>()
                .Length.Should().Be(1);
        }
        
        [Test]
        public async Task BallMoves()
        {
            var originalPosition = FindObjectOfType<BallView>().transform.position;
            
            await Task.Delay(3000);

            FindObjectOfType<BallView>().transform.position.Should().NotBe(originalPosition);
        }
    }
}
