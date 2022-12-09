using System;
using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;
using Zenject;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Input.Runtime
{
    public class DrawingInput : MonoBehaviour, DrawTrampolineInput
    {
        [Inject] Player player;

        public event Action<DrawInputReceivedArgs> DrawInputReceived;
        public event Action<Player> EndDrawInputReceived;

        public void Update()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                var position = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                SendDrawInput(position);
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                SendEndDrawInput();
            }
        }

        public void SendEndDrawInput()
        {
            EndDrawInputReceived?.Invoke(player);
        }

        public void SendDrawInput(Vector3 position)
        {
            DrawInputReceived?.Invoke(new DrawInputReceivedArgs(player, new Vector2(position.x, position.y)));
        }
    }
}
