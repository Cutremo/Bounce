using System.Collections.Generic;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Score
    {
        readonly Dictionary<Player, int> points = new();

        public Score(IEnumerable<Player> players)
        {
            foreach (var p in players)
            {
                points.Add(p, 0);
            }
        }

        public void GivePointTo(Player player)
        {
            points[player]++;
        }

        public int PointsOf(Player player) => points[player];
    }
}