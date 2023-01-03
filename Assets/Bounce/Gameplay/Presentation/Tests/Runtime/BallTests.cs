using System.Collections;
using System.Collections.Generic;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using FluentAssertions;
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
        public void METHOD()
        {
            FindObjectsOfType<BallView>()
                .Length.Should().Be(1);
        }
    }
}
