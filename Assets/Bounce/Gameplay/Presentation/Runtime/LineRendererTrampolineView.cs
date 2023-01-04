using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Runtime
{
    
    //Borrar esta clase?
    public class LineRendererTrampolineView : MonoBehaviour, TrampolineView
    {
        [SerializeField] TrampolineDrawingPanel drawingPanel;
        [SerializeField] LineRenderer trampolinePrefab;
        
        LineRenderer trampolineInstance;

        public Task Add(Player _0, Trampoline trampoline)
        {
            trampolineInstance = Instantiate(trampolinePrefab, transform);
            trampolineInstance.gameObject.name = "Trampoline";
            trampolineInstance.positionCount = 2;
            trampolineInstance.SetPosition(0, new Vector3(trampoline.Origin.X, trampoline.Origin.Y, 0));
            trampolineInstance.SetPosition(1, new Vector3(trampoline.End.X, trampoline.End.Y, 0));

            return Task.CompletedTask;
        }

        public Task RemoveCurrent(Player player)
        {
            if (trampolineInstance != null)
            {
                Destroy(trampolineInstance.gameObject);
            }
            return Task.CompletedTask;
        }
    }
}