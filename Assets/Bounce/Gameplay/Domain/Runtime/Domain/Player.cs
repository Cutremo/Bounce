using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Player
    {
        public Trampoline Trampoline => trampoline;
        
        Trampoline trampoline;
        
        readonly Sketchbook sketchbook;

        public Player(Sketchbook sketchbook)
        {
            this.sketchbook = sketchbook;
        }

        public void Draw(Vector2 end)
        {
            sketchbook.Draw(end);
            trampoline = sketchbook.Result;
        }

        public void StopDrawing()
        {
            trampoline = sketchbook.Result;
            sketchbook.StopDrawing();
        }
    }
}