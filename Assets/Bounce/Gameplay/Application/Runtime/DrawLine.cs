using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public class DrawLine 
    {
        readonly Area area;
        readonly DrawLineView drawLineView;

        public DrawLine(Area area, DrawLineView drawLineView)
        {
            this.area = area;
            this.drawLineView = drawLineView;
        }

        public async Task Draw(Vector2 end)
        {
            if (!area.InsideBounds(end))
                return;
            area.Draw(end);
            await drawLineView.Draw(area.Trampoline);
        }

        public async Task StopDrawing()
        {
            if (!area.Drawing)
                return;
            
            area.StopDrawing();
            await drawLineView.StopDrawing(area.Trampoline);
        }
    }
}
