using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Runtime
{
    public class TrampolineRenderer : MonoBehaviour, DrawTrampolineView
    {
        [SerializeField] LineRenderer lineRenderer;
        
        public Task Draw(Trampoline trampoline)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(trampoline.Origin.X, trampoline.Origin.Y, 0));
            lineRenderer.SetPosition(1, new Vector3(trampoline.End.X, trampoline.End.Y, 0));

            return Task.CompletedTask;
        }

        public Task StopDrawing(Trampoline trampoline)
        {
            lineRenderer.positionCount = 0;
            return Task.CompletedTask;
        }
    }
}