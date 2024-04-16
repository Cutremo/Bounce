using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Client.Presentation.Runtime
{
    public class UnityCollisionView : CollisionView
    {
        public Task HandleCollision()
        {
            GameObject.FindObjectOfType<CameraShake>().Shake();
            return Task.Delay(200);
        }
    }
}