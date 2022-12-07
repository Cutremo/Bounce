using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Bounce
{
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        LineRenderer lineRenderer;


        public void Build(Vector2 from, Vector2 to)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, from);
            lineRenderer.SetPosition(1, to);

            var startColor = new Color2(Color.white, Color.white);
            var endColor = new Color2(new Color(1,1,1,0), new Color(1,1,1,0));

            lineRenderer.DOColor(startColor, endColor, 1f).SetDelay(2f)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
