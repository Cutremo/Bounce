using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public interface TrampolinesView
    {
        public void Add(Trampoline trampoline);
        public Task RemoveCurrent();
    }
}