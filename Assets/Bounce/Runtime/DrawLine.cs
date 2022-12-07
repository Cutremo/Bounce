using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bounce.Runtime
{
    public class DrawLine : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] Platform platformPrefab;
        bool drawing;
        [SerializeField]
        float maxLineLength;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartDrawing();
            }

            if (Input.GetMouseButtonUp(0))
            {
                EndDrawing();
            }

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

        void EndDrawing()
        {
            drawing = false;
            Instantiate(platformPrefab, Vector3.zero, Quaternion.identity, transform)
                .Build(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
            lineRenderer.positionCount = 0;
        }

        void StartDrawing()
        {
            drawing = true;
            AddPosition();
            AddPosition();
        }

        void AddPosition()
        {
            lineRenderer.positionCount++;
            UpdateLastPosition();
        }

        void UpdateLastPosition()
        {
            lineRenderer.SetPosition(lineRenderer.positionCount - 1,
                (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
