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

        public Task Add(Player _0, Trampoline trampoline)
        {
            trampolineInstance = Instantiate(prefab, transform);
            trampolineInstance.gameObject.name = "Trampoline";
            trampolineInstance.Draw(trampoline);

            return Task.CompletedTask;
        }

        public async Task RemoveCurrent(Player player)
        {
            if (trampolineInstance != null)
            {
                await trampolineInstance.Destroy();
            }

            trampolineInstance = null;
        }
    }
}