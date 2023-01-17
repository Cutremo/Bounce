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
        protected GameObject player0;
        protected GameObject player1;

        protected DrawingInput drawingInput;
        protected LineRenderer lineRenderer;

        [UnitySetUp]
        public override IEnumerator LoadScene()
        {
            yield return base.LoadScene();
            player0 = GameObject.Find("player0");
            player1 = GameObject.Find("player1");
            drawingInput = player0.GetComponentInChildren<DrawingInput>(true);
            lineRenderer = player0.GetComponentInChildren<LineRenderer>(true);
        }

        protected DrawingInput DrawingInputOf(GameObject player)
        {
            return player.GetComponentInChildren<DrawingInput>(true);
        }
        
        protected LineRenderer LineRendererOf(GameObject player)
        {
            return player.GetComponentInChildren<LineRenderer>(true);
        }
        
        protected TMP_Text TextOf(GameObject player)
        {
            return player.GetComponentInChildren<PointsText>(true).GetComponentInChildren<TMP_Text>(true);
        }
    }
}