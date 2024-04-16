using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Client.Presentation.Runtime
{
    public class UnityCollisionView : CollisionView
    {
        public Task HandleCollision(Ball ball)
        {
            var multiplier = ball.TimesMultipliedSpeed - 1;

            if(multiplier <= 0.4f)
                return Task.CompletedTask;
            
            GameObject.FindObjectOfType<CameraShake>().Shake(multiplier);
            return Task.Delay((int)(200 * multiplier));
        }
    }
}