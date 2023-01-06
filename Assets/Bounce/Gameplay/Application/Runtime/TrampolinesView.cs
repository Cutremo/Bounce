using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public interface TrampolinesView
    {
        public Task Add(Player player, Trampoline trampoline);
        public Task RemoveCurrent(Player player);
    }
}