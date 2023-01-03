using System.Collections.Generic;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Game
    {
        readonly IDictionary<Player, Area> pairs;
        readonly int targetScore;
        readonly Score score;
        readonly Pitch pitch;
        
        bool playersEnabled;
        public bool Playing { get; private set; }

        public bool PlayersEnabled
        {
            get => playersEnabled;
            set
            {
                Require(playersEnabled != value).True();
                playersEnabled = value;
            }
        }

        public Ball Ball => pitch.Ball;

        //Pasar el int.
        public Game(Pitch pitch, IDictionary<Player, Area> pairs, int targetScore = int.MaxValue)
        {
            Require(pairs).Not.Null();
            Require(pairs).Not.Empty();
            foreach(var pair in pairs)
                Require(pitch.Contains(pair.Value)).True();

            this.pitch = pitch;
            this.pairs = pairs;
            this.targetScore = targetScore;
            score = new Score(pairs.Keys);
        }

        public void Begin()
        {
            Require(Playing).False();
            Playing = true;
        }

        public void DropBall()
        {
            var ball = new Ball(pitch.Center, Vector2.Up, 0.75f)
            {
                Speed = 1
            };

            pitch.DropBall(ball);
        }
        
        public void Draw(Player player, Vector2 end)
        {
            Require(Playing).True();
            pairs[player].Draw(end);
        }

        public Trampoline TrampolineOf(Player player)
        {
            return pairs[player].Trampoline;
        }

        public void StopDrawing(Player player)
        {
            Require(Playing).True();
            pairs[player].StopDrawing();
        }

        public bool IsDrawing(Player player)
        {
            Require(Playing).True();
            return pairs[player].Drawing;
        }

        public bool InsideBounds(Player player, Vector2 position)
        {
            return pairs[player].InsideBounds(position);
        }

        public void GivePointTo(Player player)
        {
            Require(Playing).True();
            score.GivePointTo(player);

            if (PointsOf(player) >= targetScore)
            {
                End();
            }
        }

        public void End()
        {
            Playing = false;
        }

        public int PointsOf(Player player) => score.PointsOf(player);

        public void SimulateBall(float seconds)
        {
            pitch.SimulateBall(seconds);
        }
    }
}