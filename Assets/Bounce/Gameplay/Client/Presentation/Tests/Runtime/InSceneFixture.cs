using System;
using System.Collections;
using System.Threading.Tasks;
using FluentAssertions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public abstract class InSceneFixture
    {
        [UnitySetUp]
        public virtual IEnumerator LoadScene()
        {
            SceneManager.LoadScene("MainScene");
            yield return null;
            yield return null;
            yield return new WaitForSeconds(3f);
        }

        public IEnumerator AssertThatHappensInTime(Func<bool> operation, float timeOut)
        {
            var counter = timeOut;

            while(!operation() && timeOut > 0)
            {
                timeOut -= Time.deltaTime;
                yield return null;
            }

            operation().Should().BeTrue("TimeOut");
        }
    }
}