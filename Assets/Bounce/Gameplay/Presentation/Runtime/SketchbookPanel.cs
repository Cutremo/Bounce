using System;
using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using DG.Tweening;
using JunityEngine.Maths.Runtime;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Presentation.Runtime
{
    public class SketchbookPanel : MonoBehaviour, SketchbookView
    {
        [Inject] Player player;
        [Inject] Game game;
        
        [FormerlySerializedAs("trampoline")] [SerializeField] LineRenderer drawing;
        [SerializeField] SpriteRenderer area;

        Tween tween;
        void Start()
        {
            drawing.positionCount = 0;

            var bounds = game.AreaBoundsOf(player);
            var center = bounds.Center;
            area.transform.position = new Vector3(center.X, center.Y, 0);
            area.transform.localScale = new Vector3(bounds.SizeX, bounds.SizeY, 1);
            area.gameObject.SetActive(true);
        }

        public Task BeginDraw(Vector2 position)
        {
            if(!game.AreaBoundsOf(player).Contains(position))
            {
                area.gameObject.SetActive(true);
                //Class method
                tween = DOVirtual.DelayedCall(0.5f, () => area.gameObject.SetActive(false));
            }
            area.gameObject.SetActive(true);
            return Task.CompletedTask;
        }
        public Task Draw(Trampoline trampoline)
        {
            area.gameObject.SetActive(true);

            drawing.positionCount = 2;
            drawing.SetPosition(0, new Vector3(trampoline.Origin.X, trampoline.Origin.Y, 0));
            drawing.SetPosition(1, new Vector3(trampoline.End.X, trampoline.End.Y, 0));
            
            return Task.CompletedTask;
        }
        
        public Task StopDrawing()
        {
            //Class method
            if(tween != null)
            {
                tween.Kill();
                tween = null;
            }
            
            area.gameObject.SetActive(false);
            drawing.positionCount = 0;
            return Task.CompletedTask;
        }
    }
}