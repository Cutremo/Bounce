using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Application.Runtime
{
    public class DrawLine 
    {
        readonly Game game;
        readonly DrawLineView drawLineView;

        public DrawLine(Game game, DrawLineView drawLineView)
        {
            this.game = game;
            this.drawLineView = drawLineView;
        }

        public async Task Draw(Player player, Vector2 end)
        {
            Debug.Log(end);
            Debug.Log(game.InsideBounds(player, end));
            if (!game.InsideBounds(player, end))
                return;
            
            Debug.Log(game.TrampolineOf(player));

            game.Draw(player, end);
            await drawLineView.Draw(game.TrampolineOf(player));
        }

        public async Task StopDrawing(Player player)
        {
            if (!game.IsDrawing(player))
                return;
            
            game.StopDrawing(player);
            await drawLineView.StopDrawing(game.TrampolineOf(player));
        }
    }
}
