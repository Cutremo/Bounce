namespace Bounce.Gameplay.Domain.Runtime
{
    public class Player
    {
        public string Id { get; }

        public Player(string id = "")
        {
            Id = id;
        }
    }
}