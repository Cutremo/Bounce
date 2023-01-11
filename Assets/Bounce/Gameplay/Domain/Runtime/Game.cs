using System.Collections.Generic;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Game
    {
        readonly IEnumerable<Player> players;
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
        public Game(Pitch pitch, IEnumerable<Player> players, int targetScore = int.MaxValue)
        {
            Require(players).Not.Null();
            Require(players).Not.Empty();
            this.pitch = pitch;
            this.players = players;
            this.targetScore = targetScore;
            score = new Score(players);
        }

        public void Begin()
        {
            Require(Playing).False();
            Playing = true;
        }

        public void DropBall()
        {
            //Mover esto a infraestructura o lo que sea.
            var ball = new Ball(pitch.Center, Vector2.Down.Normalize, 1f)
            {
                Speed = 4
            };

            pitch.DropBall(ball);
        }
        
        public void Draw(Player player, Vector2 end)
        {
            Require(Playing).True();
            pitch.Draw(player, end);
        }

        public Trampoline TrampolineOf(Player player)
        {
            return pitch.TrampolineOf(player);
        }

        public void StopDrawing(Player player)
        {
            Require(Playing).True();
            pitch.StopDrawing(player);
        }

        public bool IsDrawing(Player player)
        {
            Require(Playing).True();
            return pitch.Drawing(player);
        }

        public bool InsideBounds(Player player, Vector2 position) => pitch.InsideArea(player, position);

        public Bounds2D AreaBoundsOf(Player player) => pitch.AreaBoundsOf(player);

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

        public void SimulateBall(float seconds) => pitch.SimulateBall(seconds);
    }
}