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
    public class ScoringTests : InPointFixture
    {
        [UnityTest]
        public IEnumerator MayNotDrawWhenGameEnded()
        {
            yield return new WaitUntil(() => Object.FindObjectOfType<BallView>() == null);
            
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-3,0));
            LineRendererOf(player0).positionCount.Should().Be(0);
        }

        [UnityTest]
        public IEnumerator DrawingsAreCancelledWhenGameEnds()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-3,0));

            yield return new WaitUntil(() => Object.FindObjectOfType<BallView>() == null);
            
            LineRendererOf(player0).positionCount.Should().Be(0);
        }

        [UnityTest]
        public IEnumerator TrampolinesAreRemovedWhenEndingGame()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-3,0));
            DrawingInputOf(player0).SendEndDrawInput();
            
            yield return new WaitUntil(() => Object.FindObjectOfType<BallView>() == null);

            Object.FindObjectsOfType<TrampolineView>().Length.Should().Be(0);
        }
        
        [UnityTest]
        public IEnumerator BallIsRemovedWhenScoring()
        {
            yield return AssertThatHappensInTime(() => Object.FindObjectOfType<BallView>() == null, 8f);
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
            yield return AssertThatHappensInTime(() => TextOf(player0).text == "0" && TextOf(player1).text == "1", 15f);
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