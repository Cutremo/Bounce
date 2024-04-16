using System.Threading.Tasks;

namespace Bounce.Gameplay.Application.Runtime
{
    public interface CollisionView 
    {
        Task HandleCollision();
    }
}