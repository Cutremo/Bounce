using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Client.Presentation.Runtime
{
    public class UnityCollisionView : CollisionView
    {
        public async Task HandleCollision(Ball ball)
        {
            var multiplier = ball.TimesMultipliedSpeed - 1;

            if(multiplier <= 0.4f)
                return;
            
            GameObject.FindObjectOfType<CameraShake>().Shake(multiplier);
            await Task.Delay((int)(200 * multiplier));
        }
    }
}