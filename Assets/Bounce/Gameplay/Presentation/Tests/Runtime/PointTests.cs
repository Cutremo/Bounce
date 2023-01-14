using System.Threading.Tasks;
using Bounce.Gameplay.Presentation.Runtime;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public class PointTests : FirstPlayerDrawingFixture
    {
        [Test]
        public async Task MayNotDrawWhenGameEnded()
        {
            await Task.Delay(2500);
            
            drawingInput.SendDrawInput(new Vector3(0.5f,-5,0));
            drawingInput.SendDrawInput(new Vector3(0.5f,-3,0));
            lineRenderer.positionCount.Should().Be(0);
        }

        [Test]
        public async Task DrawingsAreCancelledWhenGameEnds()
        {
            await Task.Delay(4000);
            drawingInput.SendDrawInput(new Vector3(0.5f,-5,0));
            drawingInput.SendDrawInput(new Vector3(0.5f,-3,0));

            await Task.Delay(4000);
            
            drawingInput.SendDrawInput(new Vector3(0.5f,-5,0));
            drawingInput.SendDrawInput(new Vector3(0.5f,-3,0));
            lineRenderer.positionCount.Should().Be(0);
        }

        [Test]
        public async Task TrampolinesAreRemovedWhenEndingGame()
        {
            await Task.Delay(3000);
            drawingInput.SendDrawInput(new Vector3(0.5f,-5,0));
            drawingInput.SendDrawInput(new Vector3(0.5f,-3,0));
            drawingInput.SendEndDrawInput();
            await Task.Delay(4000);
            
            
            Object.FindObjectsOfType<TrampolineView>().Length.Should().Be(0);
        }
        
        [Test]
        public async Task BallIsRemovedWhenScoring()
        {
            await Task.Delay(2500);
            
            Object.FindObjectsOfType<BallView>().Length.Should().Be(0);
        }
        
        [Test]
        public async Task MayNotDrawDuringDropBallAnimation()
        {
            await Task.Yield();
            
            drawingInput.SendDrawInput(new Vector3(0.5f,-5,0));
            drawingInput.SendDrawInput(new Vector3(0.5f,-3,0));

            Object.FindObjectsOfType<TrampolineView>().Length.Should().Be(0);
        }
    
        [Test]
        public async Task NewTurnBegins()
        {
            await Task.Delay(2500);
            
            Object.FindObjectsOfType<BallView>().Length.Should().Be(0);
            
            await Task.Delay(2000);

            Object.FindObjectsOfType<BallView>().Length.Should().Be(1);
        }
    }
}