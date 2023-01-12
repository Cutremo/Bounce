using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public interface SketchbookView
    {
        public void BeginDraw(Vector2 position);
        public void Draw(Trampoline trampoline);
        public void StopDrawing();
    }
}
