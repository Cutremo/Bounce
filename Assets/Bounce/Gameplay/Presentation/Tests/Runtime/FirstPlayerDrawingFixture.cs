using System.Collections;
using Bounce.Gameplay.Input.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public abstract class FirstPlayerDrawingFixture : InSceneFixture
    {
        protected DrawingInput drawingInput;
        protected LineRenderer lineRenderer;

        [UnitySetUp]
        public override IEnumerator LoadScene()
        {
            yield return base.LoadScene();
            var player = GameObject.Find("player0");
            drawingInput = player.GetComponentInChildren<DrawingInput>(true);
            lineRenderer = player.GetComponentInChildren<LineRenderer>(true);
        }
    }
}