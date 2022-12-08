using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Input.Runtime;
using UnityEngine;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Application.Runtime
{
    public class DrawLine 
    {
        readonly Game game;
        readonly DrawLineView drawLineView;
        readonly DrawLineInput drawingInput;
        public DrawLine(Game game, DrawLineView drawLineView, DrawLineInput drawingInput)
        {
            this.game = game;
            this.drawLineView = drawLineView;
            this.drawingInput = drawingInput;
        }

        public async Task HandleDraw()
        {
            var endDrawInput = drawingInput.EndDrawInput();
            while (!endDrawInput.IsCompleted)
            {
                var drawInput = drawingInput.DrawInput();
                await drawInput;
                await Draw(drawInput.Result.Item1, drawInput.Result.Item2);
                await Task.Yield(); //Por que esto tiene que estar aqui para no petar? 
            }
            await EndDrawing(endDrawInput.Result);
        }
        
        public async Task Draw(Player player, Vector2 end)
        {
            if (!game.InsideBounds(player, end))
                return;
            
            game.Draw(player, end);
            await drawLineView.Draw(game.TrampolineOf(player));
        }

        public async Task EndDrawing(Player player)
        {
            if (!game.IsDrawing(player))
                return;
            
            game.StopDrawing(player);
            await drawLineView.StopDrawing(game.TrampolineOf(player));
        }
    }
}
