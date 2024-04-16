using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public interface CollisionView 
    {
        Task HandleCollision(Ball ball);
    }
}