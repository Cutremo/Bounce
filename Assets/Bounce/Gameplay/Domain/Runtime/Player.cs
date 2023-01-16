namespace Bounce.Gameplay.Domain.Runtime
{
    public class Player
    {
        public string Id { get; }

        public Player(string id = "")
        {
            Id = id;
        }

        protected bool Equals(Player other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj))
                return false;
            if(ReferenceEquals(this, obj))
                return true;
            if(obj.GetType() != this.GetType())
                return false;
            return Equals((Player)obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public static bool operator ==(Player left, Player right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Player left, Player right)
        {
            return !Equals(left, right);
        }
    }
}