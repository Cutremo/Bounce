using System.Collections.Generic;
using Bounce.Gameplay.Domain.Runtime;
using static Bounce.Gameplay.Domain.Tests.Builders.PitchBuilder;

namespace Bounce.Gameplay.Domain.Tests.Builders
{
    public class GameBuilder
    {
        int targetScore = 1;
        IDictionary<Player,Area> areas = new Dictionary<Player, Area>();
        Pitch pitch = Pitch().Build();
        public static GameBuilder Game() => new();

        public GameBuilder WithPitch(Pitch pitch)
        {
            this.pitch = pitch;
            return this;
        }

        public GameBuilder AddPlayer(Player player)
        {
            areas.Add(player, AreaBuilder.Area().Build());
            return this;
        }

        public GameBuilder WithPlayers(int amount)
        {
            for (int i = 0; i < amount; i++)
                areas.Add(new Player(), AreaBuilder.Area().Build());

            return this;
        }

        public GameBuilder WithTargetScore(int targetScore)
        {
            this.targetScore = targetScore;
            return this;
        }
        
        public Game Build() => new(pitch, areas, targetScore);    
    }
}