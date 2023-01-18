using System;
using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using UnityEngine;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Input.Runtime
{
    public class DrawingInput : MonoBehaviour, DrawTrampolineInput
    {
        public event Action<Vector2> DrawInputReceived;
        public event Action EndDrawInputReceived;

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
            EndDrawInputReceived?.Invoke();
        }

        public void SendDrawInput(Vector3 position)
        {
            DrawInputReceived?.Invoke(new Vector2(position.x, position.y));
        }
    }
}
