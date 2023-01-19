using System.Collections;
using Bounce.Gameplay.Input.Runtime;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public abstract class InPointFixture : GameplayFixture
    {
        [UnitySetUp]
        public override IEnumerator LoadScene()
        {
            yield return base.LoadScene();
            yield return new WaitUntil(() => Object.FindObjectOfType<BallView>() != null);
            Vector3 startPostiion = Object.FindObjectOfType<BallView>().transform.position;
            yield return new WaitUntil(() => Object.FindObjectOfType<BallView>().transform.position != startPostiion);
        }
    }
}