using System;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Input.Runtime
{
    public interface DrawTrampolineInput
    {
        public event Action<Vector2> DrawInputReceived; 
        public event Action EndDrawInputReceived;
    }
}