using System.Collections.Generic;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Score
    {
        readonly Dictionary<Player, int> scores = new();

        public Score(IEnumerable<Player> players)
        {
            foreach (var p in players)
            {
                scores.Add(p, 0);
            }
        }

        public void GivePointTo(Player player)
        {
            scores[player]++;
        }

        public int PointsOf(Player player) => scores[player];
    }
}