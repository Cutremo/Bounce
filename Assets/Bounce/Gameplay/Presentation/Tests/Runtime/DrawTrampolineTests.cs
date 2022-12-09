using System.Collections;
using System.Threading.Tasks;
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
        
        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            SceneManager.LoadScene("MainScene");
            yield return null;
        }
        
        [Test]
        public async Task DrawTrampoline()
        {
            Object.FindObjectOfType<DrawingInput>().SendDrawInput(Vector3.zero);
            Object.FindObjectOfType<DrawingInput>().SendDrawInput(Vector3.one);
            
            await Task.Yield();
            
            using var _ = new AssertionScope();
            Object.FindObjectOfType<LineRenderer>().GetPosition(0).Should().Be(Vector3.zero);
            Object.FindObjectOfType<LineRenderer>().GetPosition(1).Should().Be(new Vector3(1,1,0));
        }

        [Test]
        public async Task DrawOutOfBounds()
        {
            Object.FindObjectOfType<DrawingInput>().SendDrawInput(new Vector3(3,3,0));
            Object.FindObjectOfType<DrawingInput>().SendDrawInput(new Vector3(4,4,0));
            
            await Task.Yield();
            
            using var _ = new AssertionScope();
            Object.FindObjectOfType<LineRenderer>().GetPosition(0).Should().Be(new Vector3(3,3,0));
            Object.FindObjectOfType<LineRenderer>().GetPosition(1).Should().Be(new Vector3(4,4,0));
        }
        
        [Test]
        public async Task EndDrawTrampolineRemains()
        {
            Object.FindObjectOfType<DrawingInput>().SendDrawInput(new Vector3(3,3,0));
            Object.FindObjectOfType<DrawingInput>().SendDrawInput(new Vector3(4,4,0));
            
            Object.FindObjectOfType<DrawingInput>().SendEndDrawInput();

            await Task.Yield();
            using var _ = new AssertionScope();
            Object.FindObjectOfType<LineRenderer>().GetPosition(0).Should().Be(new Vector3(3,3,0));
            Object.FindObjectOfType<LineRenderer>().GetPosition(1).Should().Be(new Vector3(4,4,0));
        }
        
                
        [Test]
        public async Task DrawingAnotherTrampoline()
        {
            Object.FindObjectOfType<DrawingInput>().SendDrawInput(new Vector3(3,3,0));
            Object.FindObjectOfType<DrawingInput>().SendDrawInput(new Vector3(4,4,0));
            Object.FindObjectOfType<DrawingInput>().SendEndDrawInput();

            Object.FindObjectOfType<DrawingInput>().SendDrawInput(new Vector3(2,2,0));
            Object.FindObjectOfType<DrawingInput>().SendDrawInput(new Vector3(3,3,0));
            
            await Task.Delay(500);
            using var _ = new AssertionScope();
            Object.FindObjectsOfType<LineRenderer>().Should().HaveCount(1);
            Object.FindObjectOfType<LineRenderer>().GetPosition(0).Should().Be(new Vector3(2, 2,0));
            Object.FindObjectOfType<LineRenderer>().GetPosition(1).Should().Be(new Vector3(3,3,0));
        }
    }
}
