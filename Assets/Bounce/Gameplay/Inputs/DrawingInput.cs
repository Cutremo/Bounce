using System;
using System.Collections;
using System.Collections.Generic;
using Bounce.Gameplay.Application.Runtime;
using JunityEngine.Maths.Runtime;
using UnityEngine;
using Zenject;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Input.Runtime
{
    public class DrawingInput : MonoBehaviour
    {
        [Inject] DrawLine drawLine;
        
        void Update()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                Draw(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition));
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                StopDrawing();
            }
        }

        public void Draw(Vector3 position)
        {
            drawLine.Draw(new Vector2(position.x, position.y));
        }

        public void StopDrawing()
        {
            drawLine.StopDrawing();
        }
    }
}
