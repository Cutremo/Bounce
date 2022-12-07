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
            Object.FindObjectOfType<DrawingInput>().Draw(Vector3.zero);
            Object.FindObjectOfType<DrawingInput>().Draw(Vector3.one);
            
            await Task.Yield();
            
            using var _ = new AssertionScope();
            Object.FindObjectOfType<LineRenderer>().GetPosition(0).Should().Be(Vector3.zero);
            Object.FindObjectOfType<LineRenderer>().GetPosition(1).Should().Be(new Vector3(1,1,0));
        }

        [Test]
        public async Task DrawOutOfBounds()
        {
            Object.FindObjectOfType<DrawingInput>().Draw(new Vector3(3,3,0));
            Object.FindObjectOfType<DrawingInput>().Draw(new Vector3(4,4,0));
            
            await Task.Yield();
            
            using var _ = new AssertionScope();
            Object.FindObjectOfType<LineRenderer>().GetPosition(0).Should().Be(new Vector3(3,3,0));
            Object.FindObjectOfType<LineRenderer>().GetPosition(1).Should().Be(new Vector3(4,4,0));
        }
    }
}
