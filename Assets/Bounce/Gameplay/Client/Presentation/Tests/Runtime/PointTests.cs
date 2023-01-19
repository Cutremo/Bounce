using System.Collections;
using System.Threading.Tasks;
using Bounce.Gameplay.Presentation.Runtime;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public class PointTests : DrawingFixture
    {
        [UnityTest]
        public IEnumerator MayNotDrawWhenGameEnded()
        {
            yield return new WaitUntil(() => Object.FindObjectOfType<BallView>() == null);
            
            drawingInput.SendDrawInput(new Vector3(0.5f,-5,0));
            drawingInput.SendDrawInput(new Vector3(0.5f,-3,0));
            lineRenderer.positionCount.Should().Be(0);
        }

        [UnityTest]
        public IEnumerator DrawingsAreCancelledWhenGameEnds()
        {
            drawingInput.SendDrawInput(new Vector3(0.5f,-5,0));
            drawingInput.SendDrawInput(new Vector3(0.5f,-3,0));

            yield return new WaitUntil(() => Object.FindObjectOfType<BallView>() == null);
            
            lineRenderer.positionCount.Should().Be(0);
        }

        [UnityTest]
        public IEnumerator TrampolinesAreRemovedWhenEndingGame()
        {
            drawingInput.SendDrawInput(new Vector3(0.5f,-5,0));
            drawingInput.SendDrawInput(new Vector3(0.5f,-3,0));
            drawingInput.SendEndDrawInput();
            
            yield return new WaitUntil(() => Object.FindObjectOfType<BallView>() == null);

            Object.FindObjectsOfType<TrampolineView>().Length.Should().Be(0);
        }
        
        [UnityTest]
        public IEnumerator BallIsRemovedWhenScoring()
        {
            yield return AssertThatHappensInTime(() => Object.FindObjectOfType<BallView>() == null, 8f);
        }
        
        [Test]
        public async Task MayNotDrawDuringDropBallAnimation() //this shouldnt be here
        {
            drawingInput.SendDrawInput(new Vector3(0.5f,-5,0));
            drawingInput.SendDrawInput(new Vector3(0.5f,-3,0));

            Object.FindObjectsOfType<TrampolineView>().Length.Should().Be(0);
        }
    
        [UnityTest]
        public IEnumerator NewTurnBegins()
        {
            yield return new WaitUntil(() => Object.FindObjectOfType<BallView>() == null);
            
            yield return AssertThatHappensInTime(() => Object.FindObjectOfType<BallView>() != null, 3f);
        }
        
        [UnityTest]
        public IEnumerator ChangesScoreWhenPointEnded()
        {
            yield return AssertThatHappensInTime(() => TextOf(player0).text == "0" && TextOf(player1).text == "1", 7f);
        }
        
        [Test]
        public void DefaultPointsAreZero()
        {
            using var _ = new AssertionScope();
            TextOf(player0).text.Should().Be("0");
            TextOf(player1).text.Should().Be("0");
        }
    }
}