using System.Collections.Generic;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Game
    {
        readonly IDictionary<Player, Area> areas;
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
        public Game(Pitch pitch, IDictionary<Player, Area> areas, int targetScore = int.MaxValue)
        {
            Require(areas).Not.Null();
            Require(areas).Not.Empty();
            foreach(var pair in areas)
                Require(pitch.Contains(pair.Value)).True();

            this.pitch = pitch;
            this.areas = areas;
            this.targetScore = targetScore;
            score = new Score(areas.Keys);
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
            areas[player].Draw(end);
        }

        public Trampoline TrampolineOf(Player player)
        {
            return areas[player].Trampoline;
        }

        public void StopDrawing(Player player)
        {
            Require(Playing).True();
            areas[player].StopDrawing();
        }

        public bool IsDrawing(Player player)
        {
            Require(Playing).True();
            return areas[player].Drawing;
        }

        public bool InsideBounds(Player player, Vector2 position)
        {
            return areas[player].InsideBounds(position);
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

        public Bounds2D AreaBoundsOf(Player player) => areas[player].Bounds;
    }
}