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
    public class DrawTrampolineTests : DrawingFixture
    {
        [Test]
        public async Task DrawTrampoline()
        {
            drawingInput.SendDrawInput(new Vector3(3,-5,0));
            drawingInput.SendDrawInput(new Vector3(4,-5,0));
            
            await Task.Yield();
            
            using var _ = new AssertionScope();
            lineRenderer.GetPosition(0).Should().Be(new Vector3(3,-5,0));
            lineRenderer.GetPosition(1).Should().Be(new Vector3(4,-5,0));
        }

        [Test]
        public async Task DrawOutOfBounds()
        {
            drawingInput.SendDrawInput(new Vector3(4,-4,0));
            drawingInput.SendDrawInput(new Vector3(7,-4,0));
            
            await Task.Yield();
            
            using var _ = new AssertionScope();
            // lineRenderer.positionCount.Should().Be(1);
            lineRenderer.GetPosition(0).Should().Be(new Vector3(4,-4,0));
            lineRenderer.GetPosition(0).Should().Be(new Vector3(4,-4,0));
        }
        
        [UnityTest]
        public IEnumerator EndDrawTrampolineRemains()
        {
            drawingInput.SendDrawInput(new Vector3(4,-4,0));
            drawingInput.SendDrawInput(new Vector3(3,-4,0));
            
            drawingInput.SendEndDrawInput();

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
            drawingInput.SendDrawInput(new Vector3(3,-5,0));
            drawingInput.SendDrawInput(new Vector3(4,-5,0));
            drawingInput.SendEndDrawInput();

            drawingInput.SendDrawInput(new Vector3(4,-4,0));
            drawingInput.SendDrawInput(new Vector3(3,-4,0));

            await Task.Yield();
            using var _ = new AssertionScope();
            lineRenderer.GetPosition(0).Should().Be(new Vector3(4,-4,0));
            lineRenderer.GetPosition(1).Should().Be(new Vector3(3,-4,0));
        }

        [Test]
        public void DrawTrampolineMinSize()
        {
            drawingInput.SendDrawInput(new Vector3(3,-5,0));
            drawingInput.SendDrawInput(new Vector3(3,-4.5f,0));
            drawingInput.SendEndDrawInput();
            
            using var _ = new AssertionScope();
            GameObject.Find("player0").GetComponentsInChildren<LineRenderer>()[1].GetPosition(0)
                .Should().Be(new Vector3(3,-5.25f,0));
            GameObject.Find("player0").GetComponentsInChildren<LineRenderer>()[1].GetPosition(1)
                .Should().Be(new Vector3(3,-4.25f,0));
        }

        [Test]
        public async Task DrawingADotDoesNotThrow()
        {
            drawingInput.SendDrawInput(new Vector3(3,-5,0));
            drawingInput.SendEndDrawInput();

            await Task.Yield();
        }

        [Test]
        public void ShowsAreaWhenDrawing()
        {
            drawingInput.SendDrawInput(new Vector3(3,-5,0));
            
            using var _ = new AssertionScope();
            GameObject.Find("player0").GetComponentInChildren<SketchbookPanel>()
                .GetComponentInChildren<SpriteRenderer>().gameObject.activeInHierarchy.Should().BeTrue();
        }
        
        [Test]
        public async Task DrawingAreaShowsExample()
        {
            using var _ = new AssertionScope();

            await Task.Yield();
            
            GameObject.Find("player0").GetComponentInChildren<SketchbookPanel>()
                .GetComponentInChildren<SpriteRenderer>(true).gameObject.activeInHierarchy.Should().BeTrue();
        }
        
        [Test]
        public void AreaHiddenWhenNotDrawing()
        {
            drawingInput.SendDrawInput(new Vector3(3,-5,0));
            drawingInput.SendEndDrawInput();
            
            using var _ = new AssertionScope();
            GameObject.Find("player0").GetComponentInChildren<SketchbookPanel>()
                .GetComponentInChildren<SpriteRenderer>(true).gameObject.activeInHierarchy.Should().BeFalse();
        }
        
        [Test][Ignore("Uncertain about this feature")]
        public async Task AreaShowsForABriefTime_WhenDrawingOutOfBounds()
        {
            drawingInput.SendDrawInput(new Vector3(20,-5,0));

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
            drawingInput.SendDrawInput(new Vector3(-1,-5,0));
            drawingInput.SendDrawInput(new Vector3(1,-5f,0));
            drawingInput.SendEndDrawInput();
            
            yield return AssertThatHappensInTime(() => Object.FindObjectOfType<TrampolineView>() == null, 3f);
        }
    }
}
