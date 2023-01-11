using System.Collections.Generic;
using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Domain.Tests.Builders
{
    public class PitchBuilder
    {
        Bounds2D bounds = Bounds2D.Infinite;
        
        IDictionary<Player, Area> players = new Dictionary<Player, Area>();
        public static PitchBuilder Pitch() => new PitchBuilder();

        public Pitch Build() => new Pitch(new Field(bounds), players);

        public PitchBuilder AddPlayer(Player player, Area area)
        {
            players.Add(player, area);
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
                players.Add(new Player(), AreaBuilder.Area().Build());

            return this;
        }
    }
}