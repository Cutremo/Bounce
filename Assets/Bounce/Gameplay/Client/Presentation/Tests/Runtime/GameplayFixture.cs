using System;
using System.Collections;
using System.Threading.Tasks;
using Bounce.Gameplay.Input.Runtime;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using FluentAssertions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public abstract class GameplayFixture
    {
        protected BallView Ball => Object.FindObjectOfType<BallView>();

        protected GameObject player0;
        protected GameObject player1;
        
        [UnitySetUp]
        public virtual IEnumerator LoadScene()
        {
            SceneManager.LoadScene("MainScene");
            yield return null;
            player0 = GameObject.Find("player0");
            player1 = GameObject.Find("player1");
            
        }

        public IEnumerator AssertThatHappensInTime(Func<bool> operation, float timeOut)
        {
            while(!operation() && timeOut > 0)
            {
                timeOut -= Time.deltaTime;
                yield return null;
            }

            operation().Should().BeTrue("If not, it timed out.");
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
        
        protected bool BallExists()
        {
            return Ball != null;
        }
    }
}