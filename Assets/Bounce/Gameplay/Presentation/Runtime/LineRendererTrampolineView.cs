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
        GameObject trampoline;
        

        public Task Add(Player _0, Trampoline _1)
        {
            trampoline = Instantiate(drawingPanel.Trampoline.gameObject, transform);
            return Task.CompletedTask;
        }

        public Task RemoveCurrent(Player player)
        {
            if (trampoline != null)
            {
                Destroy(trampoline.gameObject);
            }
            return Task.CompletedTask;
        }
    }
}