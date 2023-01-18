using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Runtime
{
    
    //Borrar esta clase?
    public class UnityTrampolinesView : MonoBehaviour, TrampolinesView
    {
        [SerializeField] TrampolineView prefab;
        
        TrampolineView trampolineInstance;

        public void Add(Trampoline trampoline)
        {
            trampolineInstance = Instantiate(prefab, transform);
            trampolineInstance.gameObject.name = "Trampoline";
            trampolineInstance.Draw(trampoline);
        }

        public void RemoveCurrent()
        {
            var instanceToRemove = trampolineInstance;
            trampolineInstance = null;
            
            if (instanceToRemove != null)
                instanceToRemove.Destroy();
        }
    }
}