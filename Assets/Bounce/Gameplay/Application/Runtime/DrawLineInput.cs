using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Input.Runtime
{
    public interface DrawLineInput
    {
        public Task<(Player, Vector2)> DrawInput();
        public Task<Player> EndDrawInput();
    }
}