using System.Collections;
using Bounce.Gameplay.Input.Runtime;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public abstract class DrawingFixture : InSceneFixture
    {
        protected DrawingInput drawingInput;
        protected LineRenderer lineRenderer;

        [UnitySetUp]
        public override IEnumerator LoadScene()
        {
            yield return base.LoadScene();

            drawingInput = player0.GetComponentInChildren<DrawingInput>(true);
            lineRenderer = player0.GetComponentInChildren<LineRenderer>(true);
        }
    }
}