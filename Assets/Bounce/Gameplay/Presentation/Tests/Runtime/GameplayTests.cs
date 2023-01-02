using System.Collections;
using System.Collections.Generic;
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
    public class GameplayTests
    {
        DrawingInput drawingInput;
        LineRenderer lineRenderer;

        
        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            SceneManager.LoadScene("MainScene");
            yield return null;
            yield return null;
        }
        
        [Test]
        public async Task SecondPlayerPlays()
        {
            GameObject.Find("player0").GetComponentInChildren<DrawingInput>().SendDrawInput(new Vector3(3,3,0));
            GameObject.Find("player0").GetComponentInChildren<DrawingInput>().SendDrawInput(new Vector3(4,4,0));

            GameObject.Find("player1").GetComponentInChildren<DrawingInput>().SendDrawInput(new Vector3(-1,-1,0));
            GameObject.Find("player1").GetComponentInChildren<DrawingInput>().SendDrawInput(new Vector3(-2,-2,0));

            await Task.Delay(500);
            using var _ = new AssertionScope();
            GameObject.Find("player0").GetComponentInChildren<LineRenderer>().GetPosition(0).Should().Be(new Vector3(3, 3, 0));
            GameObject.Find("player0").GetComponentInChildren<LineRenderer>().GetPosition(1).Should().Be(new Vector3(4, 4, 0));
            GameObject.Find("player1").GetComponentInChildren<LineRenderer>().GetPosition(0).Should().Be(new Vector3(-1,-1,0));
            GameObject.Find("player1").GetComponentInChildren<LineRenderer>().GetPosition(1).Should().Be(new Vector3(-2,-2,0));
        }
    }
}