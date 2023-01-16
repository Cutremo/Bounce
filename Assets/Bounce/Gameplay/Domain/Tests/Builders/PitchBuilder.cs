using System.Collections.Generic;
using System.Linq;
using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;
using static Bounce.Gameplay.Domain.Tests.Builders.AreaBuilder;

namespace Bounce.Gameplay.Domain.Tests.Builders
{
    public class PitchBuilder
    {
        Bounds2D bounds = Bounds2D.Infinite;
        
        IDictionary<Player, Area> players = new Dictionary<Player, Area>();

        public static PitchBuilder Pitch() => new();

        public Pitch Build() => new(new Field(bounds), players);

        public PitchBuilder WithScore(Score score)
        {
            return this;
        }
        public PitchBuilder AddArea(Player player, Area area)
        {
            players.Add(player, area);
            return this;
        }
        
        public PitchBuilder AddArea(Player player)
        {
            players.Add(player, Area().Build());
            return this;
        }
        public PitchBuilder WithBounds(Bounds2D bounds)
        {
            this.bounds = bounds;
            return this;
        }

        public PitchBuilder WithPlayers(int amount)
        {
            for (int i = 0; i < amount; i++)
                players.Add(new Player(), Area().Build());

            return this;
        }
    }
}