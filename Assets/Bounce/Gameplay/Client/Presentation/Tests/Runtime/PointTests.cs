using System.Threading.Tasks;
using Bounce.Gameplay.Presentation.Runtime;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Tests.Runtime
{
    public class PointTests : DrawingFixture
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
            await Task.Delay(7000);
            
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
            await Task.Delay(7000);
            
            Object.FindObjectsOfType<BallView>().Length.Should().Be(0);
            
            await Task.Delay(2000);

            Object.FindObjectsOfType<BallView>().Length.Should().Be(1);
        }
        
        [Test]
        public async Task ChangesScoreWhenPointEnded()
        {
            await Task.Delay(7000);

            using var _ = new AssertionScope();
            TextOf(player0).text.Should().Be("0");
            TextOf(player1).text.Should().Be("1");
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