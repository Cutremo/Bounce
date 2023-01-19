using System;
using System.Collections;
using System.Threading.Tasks;
using Bounce.Gameplay.Presentation.Runtime;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public class DrawTrampolineTests : InPointFixture
    {
        [Test]
        public async Task DrawTrampoline()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(3,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(4,-5,0));
            
            await Task.Yield();
            
            using var _ = new AssertionScope();
            LineRendererOf(player0).GetPosition(0).Should().Be(new Vector3(3,-5,0));
            LineRendererOf(player0).GetPosition(1).Should().Be(new Vector3(4,-5,0));
        }

        [Test]
        public void DrawOutOfBounds()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(4,-4,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(7,-4,0));

            using var _ = new AssertionScope();
            LineRendererOf(player0).GetPosition(0).Should().Be(new Vector3(4,-4,0));
            LineRendererOf(player0).GetPosition(0).Should().Be(new Vector3(4,-4,0));
        }
        
        [UnityTest]
        public IEnumerator EndDrawTrampolineRemains()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(4,-4,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(3,-4,0));
            
            DrawingInputOf(player0).SendEndDrawInput();

            yield return new WaitUntil(
                () => GameObject.Find("player0").GetComponentInChildren<TrampolineView>() != null);
            
            using var _ = new AssertionScope();
            GameObject.Find("player0").GetComponentInChildren<TrampolineView>()
                .GetComponent<LineRenderer>().GetPosition(0)
                .Should().Be(new Vector3(4,-4,0));
            GameObject.Find("player0").GetComponentInChildren<TrampolineView>()
                .GetComponent<LineRenderer>().GetPosition(1)
                .Should().Be(new Vector3(3,-4,0));
        }
        
                
        [Test]
        public async Task DrawingAnotherTrampoline()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(3,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(4,-5,0));
            DrawingInputOf(player0).SendEndDrawInput();

            DrawingInputOf(player0).SendDrawInput(new Vector3(4,-4,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(3,-4,0));

            await Task.Yield();
            using var _ = new AssertionScope();
            LineRendererOf(player0).GetPosition(0).Should().Be(new Vector3(4,-4,0));
            LineRendererOf(player0).GetPosition(1).Should().Be(new Vector3(3,-4,0));
        }

        [Test]
        public void DrawTrampolineMinSize()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(3,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(3,-4.5f,0));
            DrawingInputOf(player0).SendEndDrawInput();
            
            using var _ = new AssertionScope();
            GameObject.Find("player0").GetComponentsInChildren<LineRenderer>()[1].GetPosition(0)
                .Should().Be(new Vector3(3,-5.25f,0));
            GameObject.Find("player0").GetComponentsInChildren<LineRenderer>()[1].GetPosition(1)
                .Should().Be(new Vector3(3,-4.25f,0));
        }

        [Test]
        public async Task DrawingADotDoesNotThrow()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(3,-5,0));
            DrawingInputOf(player0).SendEndDrawInput();

            await Task.Yield();
        }

        [Test]
        public void ShowsAreaWhenDrawing()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(3,-5,0));
            
            using var _ = new AssertionScope();
            GameObject.Find("player0").GetComponentInChildren<SketchbookPanel>()
                .GetComponentInChildren<SpriteRenderer>().gameObject.activeInHierarchy.Should().BeTrue();
        }
        
        [Test]
        public void DrawingAreaShowsExample()
        {
            GameObject.Find("player0").GetComponentInChildren<SketchbookPanel>()
                .GetComponentInChildren<SpriteRenderer>(true).gameObject.activeInHierarchy.Should().BeTrue();
        }
        
        [Test]
        public void AreaHiddenWhenNotDrawing()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(3,-5,0));
            DrawingInputOf(player0).SendEndDrawInput();
            
            using var _ = new AssertionScope();
            GameObject.Find("player0").GetComponentInChildren<SketchbookPanel>()
                .GetComponentInChildren<SpriteRenderer>(true).gameObject.activeInHierarchy.Should().BeFalse();
        }
        
        [Test][Ignore("Uncertain about this feature")]
        public async Task AreaShowsForABriefTime_WhenDrawingOutOfBounds()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(20,-5,0));

            await Task.Yield();
            using var _ = new AssertionScope();
            GameObject.Find("player0").GetComponentInChildren<SketchbookPanel>()
                .GetComponentInChildren<SpriteRenderer>(true).gameObject.activeInHierarchy.Should().BeTrue();

            await Task.Delay(5000);
            
            GameObject.Find("player0").GetComponentInChildren<SketchbookPanel>()
                .GetComponentInChildren<SpriteRenderer>(true).gameObject.activeInHierarchy.Should().BeFalse();
        }
        
        [UnityTest]
        public IEnumerator BounceRemovesTrampoline()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(-1,-5,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(1,-5f,0));
            DrawingInputOf(player0).SendEndDrawInput();
            
            yield return AssertThatHappensInTime(() => Object.FindObjectOfType<TrampolineView>() == null, 3f);
        }
        
        [Test]
        public void SecondPlayerDrawsPlays()
        {
            DrawingInputOf(player0).SendDrawInput(new Vector3(-3,-3,0));
            DrawingInputOf(player0).SendDrawInput(new Vector3(-4,-4,0));

            DrawingInputOf(player1).SendDrawInput(new Vector3(3,3,0));
            DrawingInputOf(player1).SendDrawInput(new Vector3(4,4,0));

            using var _ = new AssertionScope();
            LineRendererOf(player0).GetPosition(0).Should().Be(new Vector3(-3,-3,0));
            LineRendererOf(player0).GetPosition(1).Should().Be(new Vector3(-4,-4, 0));
            LineRendererOf(player1).GetPosition(0).Should().Be(new Vector3(3,3,0));
            LineRendererOf(player1).GetPosition(1).Should().Be(new Vector3(4,4,0));
        }
    }
}
