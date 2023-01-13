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
        
        
        // [UnityTearDown]
        // public IEnumerator UnloadScene()
        // {
        //     yield return null;
        //     Action unload = () => SceneManager.LoadScene("Empty");
        //     unload.Should().Throw<OperationCanceledException>();
        // }
    }
}