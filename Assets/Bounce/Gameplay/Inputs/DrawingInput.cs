using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;
using Zenject;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Input.Runtime
{
    public class DrawingInput : MonoBehaviour, DrawLineInput
    {
        [Inject] Player player;
        [Inject] DrawLine drawLine;

        //Como testear esto sin llamadas directas?
        public async Task<(Player, Vector2)> DrawInput()
        {
            while (!UnityEngine.Input.GetMouseButton(0))
            {
                await Task.Yield();
            }

            var position = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            return (player, new Vector2(position.x, position.y));
        }

        public async Task<Player> EndDrawInput()
        {
            while (!UnityEngine.Input.GetMouseButtonUp(0))
            {
                await Task.Yield();
            }
            
            return player;
        }

        public async Task Draw(Vector3 position)
        {
            await drawLine.Draw(player, new Vector2(position.x, position.y));
        }
        
        public async Task StopDrawing()
        {
            await drawLine.EndDrawing(player);
        }
    }
}
