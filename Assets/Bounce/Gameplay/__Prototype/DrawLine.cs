using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Bounce.Runtime
{
    public class DrawLine : MonoBehaviour
    {
        [SerializeField]
        LineRenderer lineRenderer;
        [SerializeField]
        Platform platformPrefab;
        [SerializeField]
        float maxLineLength;
        bool drawing;
        public Vector3 StartPosition { get; private set; }
        public Vector3 EndPosition { get; set; }

        [SerializeField]
        DrawInput drawInput;
        

        void Update()
        {
            drawInput.Input();

            if (drawing)
            {
                UpdateLastPosition();

                var initialPos = lineRenderer.GetPosition(0);
                var touchPos = lineRenderer.GetPosition(1);
                if (Vector3.Distance(initialPos, touchPos) >= maxLineLength)
                {
                    var directionToOrigin = (initialPos - touchPos).normalized;
                    lineRenderer.SetPosition(0, touchPos + directionToOrigin * maxLineLength);
                }
            }
        }

        public void EndDrawing()
        {
            drawing = false;
            Object.Instantiate(platformPrefab, Vector3.zero, Quaternion.identity)
                .Build(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
            lineRenderer.positionCount = 0;
        }

        public void StartDrawing()
        {
            drawing = true;
            AddPosition();
            AddPosition();
        }

        public void AddPosition()
        {
            lineRenderer.positionCount++;
            UpdateLastPosition();
        }

        public void UpdateLastPosition()
        {
            lineRenderer.SetPosition(lineRenderer.positionCount - 1,
                (Vector2) Camera.main.ScreenToWorldPoint(EndPosition));
        }

        public void UpdateCursor(Vector3 pos)
        {
            EndPosition = pos;
        }
    }
}
