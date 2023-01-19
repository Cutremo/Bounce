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
        public IEnumerator MayNotDrawWhenGamePoint()
        {
            yield return new WaitUntil(Scored);
            
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-3,0));
            LineRendererOf(player0).positionCount.Should().Be(0);
        }

        [UnityTest]
        public IEnumerator DrawingsAreCancelledWhenPointEnds()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-3,0));

            yield return new WaitUntil(Scored);
            
            LineRendererOf(player0).positionCount.Should().Be(0);
        }

        [UnityTest]
        public IEnumerator TrampolinesAreRemovedWhenPoint()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(0.5f,-3,0));
            DrawingInputOf(player0).SendEndDrawInput();
            
            yield return new WaitUntil(Scored);

            Object.FindObjectsOfType<TrampolineView>().Length.Should().Be(0);
        }
        
        [UnityTest]
        public IEnumerator BallIsRemovedWhenScoring()
        {
            yield return AssertThatHappensInTime(() => Ball == null, 8f);
        }

        [UnityTest]
        public IEnumerator NewPointBeginsAutomatically()
        {
            yield return new WaitUntil(Scored);
            
            yield return AssertThatHappensInTime(PlayingPoint, 5f);
        }
        
        [UnityTest]
        public IEnumerator ChangesScoreWhenPointEnded()
        {
            yield return new WaitUntil(Scored);

            yield return AssertThatHappensInTime(() => TextOf(player0).text == "0" && TextOf(player1).text == "1", 10f);
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