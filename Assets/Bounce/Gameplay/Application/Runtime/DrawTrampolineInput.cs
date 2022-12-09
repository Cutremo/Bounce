using System;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Input.Runtime
{
    public interface DrawTrampolineInput
    {
        public event Action<DrawInputReceivedArgs> DrawInputReceived; 
        public event Action<Player> EndDrawInputReceived;
    }
}