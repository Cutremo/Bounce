using System.Collections.Generic;
using Bounce.Gameplay.Domain.Runtime;
using NUnit.Framework;
using static Bounce.Gameplay.Domain.Tests.Builders.PitchBuilder;

namespace Bounce.Gameplay.Domain.Tests.Builders
{
    public class GameBuilder
    {
        int targetScore = 1;
        IList<Player> players = new List<Player>();
        Pitch pitch = Pitch().Build();
        public static GameBuilder Game() => new();

        public GameBuilder WithPitch(Pitch pitch)
        {
            this.pitch = pitch;
            return this;
        }

        public GameBuilder AddPlayer(Player player)
        {
            players.Add(player);
            return this;
        }

        public GameBuilder WithPlayers(int amount)
        {
            for (int i = 0; i < amount; i++)
                players.Add(new Player());

            return this;
        }

        public GameBuilder WithTargetScore(int targetScore)
        {
            this.targetScore = targetScore;
            return this;
        }
        
        public Game Build() => new(pitch, players, targetScore);    
    }
}