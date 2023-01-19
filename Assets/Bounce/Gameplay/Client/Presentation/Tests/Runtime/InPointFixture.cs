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
        Vector3 ballStartPosition;

        protected bool firstPointStarted;
        [UnitySetUp]
        public override IEnumerator LoadScene()
        {
            yield return base.LoadScene();
            yield return new WaitUntil(BallExists);
            ballStartPosition = Ball.transform.position;
            yield return new WaitUntil(PlayingPoint);
            firstPointStarted = true;
        }
        
        public bool PlayingPoint()
        {
            return BallExists() && Ball.transform.position != ballStartPosition;
        }

        protected bool Scored()
        {
            return Ball == null && firstPointStarted;
        }
    }
}