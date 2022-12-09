using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Input.Runtime
{
    public class DrawInputReceivedArgs
    {
        public Player Actor { get; }
        public Vector2 Position { get; }
        
        public DrawInputReceivedArgs(Player actor, Vector2 position)
        {
            Actor = actor;
            Position = position;
        }
    }
}