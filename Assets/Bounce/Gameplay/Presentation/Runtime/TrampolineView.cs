using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Runtime
{
    public class TrampolineView : MonoBehaviour
    {
        [SerializeField]
        LineRenderer lineRenderer;

        public void Draw(Trampoline trampoline)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(trampoline.Origin.X, trampoline.Origin.Y, 0));
            lineRenderer.SetPosition(1, new Vector3(trampoline.End.X, trampoline.End.Y, 0));
        }

        public Task Destroy()
        {
            Destroy(gameObject);
            
            return Task.CompletedTask;
        }
    }
}