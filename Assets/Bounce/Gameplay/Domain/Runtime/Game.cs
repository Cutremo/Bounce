using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool PlayingPoint { get; private set; }
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
        public Score Score => score;
        public bool CollidedWithTrampoline => pitch.CollidedWithTrampoline;

        public void HandleCollision() => pitch.HandleCollision();

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
            pitch.PlayerReceivedGoal += EndPoint;
        }

        public void BeginPoint()
        {
            PlayingPoint = true;
        }

        public void DropBall(Ball ball)
        {
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


        public void EndPoint(Player player)
        {
            PlayingPoint = false;
            foreach(var p in players.Where(x => x != player))
                GivePointToPlayer(p);
        }
        
        void GivePointToPlayer(Player player)
        {
            Require(Playing).True();
            score.GivePointTo(player);

            if (PointsOf(player) >= targetScore)
                End();
        }

        public void End()
        {
            Playing = false;
            PlayingPoint = false;
            pitch.PlayerReceivedGoal -= EndPoint;
        }

        public int PointsOf(Player player) => score.PointsOf(player);

        public void SimulateBall(float seconds) => pitch.SimulateBall(seconds);

        public void RemoveBall()
        {
            pitch.RemoveBall();
        }
    }
}