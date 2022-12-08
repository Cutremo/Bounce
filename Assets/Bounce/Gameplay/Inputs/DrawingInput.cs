using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;
using Zenject;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Input.Runtime
{
    public class DrawingInput : MonoBehaviour
    {
        [Inject] DrawLine drawLine;
        [Inject] Player player;
        
        async void Update()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                Debug.Log("asdasdadsasd");
                await Draw(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition));
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                await StopDrawing();
            }
        }

        public async Task Draw(Vector3 position)
        {
            Debug.Log("asdasdadsasd");
            await drawLine.Draw(player, new Vector2(position.x, position.y));
        }

        public async Task StopDrawing()
        {
            await drawLine.StopDrawing(player);
        }
    }
}
