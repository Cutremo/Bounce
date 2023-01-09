using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public interface SketchbookView
    {
        public Task BeginDraw(Vector2 position);
        public Task Draw(Trampoline trampoline);
        public Task StopDrawing();
    }
}
