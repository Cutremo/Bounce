using System;
using Bounce.Gameplay.Input.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Server.Runtime
{
    internal class ServerDrawTrampolineInput : DrawTrampolineInput
    {
        public event Action<Vector2> DrawInputReceived;
        public event Action EndDrawInputReceived;
    }
}