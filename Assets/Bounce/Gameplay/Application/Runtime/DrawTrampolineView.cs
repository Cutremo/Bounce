using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public interface DrawTrampolineView
    {
        public Task Draw(Trampoline trampoline);
        public Task StopDrawing(Trampoline trampoline);
    }
}
