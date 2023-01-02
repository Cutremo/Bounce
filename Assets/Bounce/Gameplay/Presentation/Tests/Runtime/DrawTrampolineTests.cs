using System.Collections;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Input.Runtime;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public class DrawTrampolineTests
    {
        DrawingInput drawingInput;
        LineRenderer lineRenderer;

        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            SceneManager.LoadScene("MainScene");
            yield return null;
            var player = GameObject.Find("player0");
            drawingInput = player.GetComponentInChildren<DrawingInput>();
            lineRenderer = player.GetComponentInChildren<LineRenderer>();
            yield return null;
        }
        
        [Test]
        public async Task DrawTrampoline()
        {
            drawingInput.SendDrawInput(Vector3.zero);
            drawingInput.SendDrawInput(Vector3.one);
            
            await Task.Yield();
            
            using var _ = new AssertionScope();
            lineRenderer.GetPosition(0).Should().Be(Vector3.zero);
            lineRenderer.GetPosition(1).Should().Be(new Vector3(1,1,0));
        }

        [Test]
        public async Task DrawOutOfBounds()
        {
            drawingInput.SendDrawInput(new Vector3(3,3,0));
            drawingInput.SendDrawInput(new Vector3(4,4,0));
            
            await Task.Yield();
            
            using var _ = new AssertionScope();
            lineRenderer.GetPosition(0).Should().Be(new Vector3(3,3,0));
            lineRenderer.GetPosition(1).Should().Be(new Vector3(4,4,0));
        }
        
        [Test]
        public async Task EndDrawTrampolineRemains()
        {
            drawingInput.SendDrawInput(new Vector3(3,3,0));
            drawingInput.SendDrawInput(new Vector3(4,4,0));
            
            drawingInput.SendEndDrawInput();

            await Task.Delay(500);
            
            using var _ = new AssertionScope();
            GameObject.Find("player0").GetComponentsInChildren<LineRenderer>()[1].GetPosition(0).Should().Be(new Vector3(3, 3,0));
            GameObject.Find("player0").GetComponentsInChildren<LineRenderer>()[1].GetPosition(1).Should().Be(new Vector3(4,4,0));
        }
        
                
        [Test]
        public async Task DrawingAnotherTrampoline()
        {
            drawingInput.SendDrawInput(new Vector3(3,3,0));
            drawingInput.SendDrawInput(new Vector3(4,4,0));
            drawingInput.SendEndDrawInput();

            drawingInput.SendDrawInput(new Vector3(2,2,0));
            drawingInput.SendDrawInput(new Vector3(3,3,0));
            
            await Task.Delay(500);
            using var _ = new AssertionScope();
            lineRenderer.GetPosition(0).Should().Be(new Vector3(2, 2,0));
            lineRenderer.GetPosition(1).Should().Be(new Vector3(3,3,0));
        }
    }
}
