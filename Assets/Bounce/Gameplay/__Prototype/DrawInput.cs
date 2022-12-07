using UnityEngine;

namespace Bounce.Runtime
{
    public class DrawInput : MonoBehaviour
    {
        [SerializeField]
        DrawLine drawLine;

        public void Input()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                drawLine.StartDrawing();
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                drawLine.EndDrawing();
            }

            drawLine.UpdateCursor(UnityEngine.Input.mousePosition);
        }
    }
}