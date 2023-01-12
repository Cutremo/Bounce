using System.Collections;
using Bounce.Gameplay.Input.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public abstract class DrawingTests
    {
        protected DrawingInput drawingInput;
        protected LineRenderer lineRenderer;

        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            SceneManager.LoadScene("MainScene");
            yield return null;
            yield return null;
            var player = GameObject.Find("player0");
            drawingInput = player.GetComponentInChildren<DrawingInput>(true);
            lineRenderer = player.GetComponentInChildren<LineRenderer>(true);
            yield return null;
        }
    }
}