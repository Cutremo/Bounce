using System;
using System.Collections.Generic;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Pitch
    {
        public event Action<Player> PlayerReceivedGoal;
        
        readonly Field field;
        readonly IDictionary<Player, Area> playerAreas;

        public Pitch(Field field, IDictionary<Player, Area> playerAreas)
        { 
            this.field = field;
            this.playerAreas = playerAreas;
        }

        public Vector2 Center => field.Center;
        public Ball Ball => field.Ball;

        public void Draw(Player player, Vector2 position)
        {
            Require(playerAreas.Keys.Contains(player)).True();
            playerAreas[player].Draw(position);
        }

        public void DropBall(Ball ball)
        {
            field.DropBall(ball);
        }

        public void SimulateBall(float time)
        {
            var previousPosition = field.Ball.Position;
            field.SimulateBall(time);
            foreach(var playerArea in playerAreas)
            {
                if(playerArea.Value.Trampoline.Completed)
                {
                    playerArea.Value.HandleCollision(field.Ball, previousPosition);
                }

                if(playerArea.Value.Scores(Ball))
                {
                    PlayerReceivedGoal?.Invoke(playerArea.Key);
                    break;
                }
            }
        }

        public void Clear()
        {
            foreach(var area in playerAreas.Values)
            {
                if(area.Trampoline != Trampoline.Null)
                    area.Clear();
            }

            field.RemoveBall();
        }
        public bool Contains(Area area)
        {
            return field.Contains(area);
        }

        public bool InsideArea(Player player, Vector2 position) => playerAreas[player].InsideBounds(position);

        public Bounds2D AreaBoundsOf(Player player) => playerAreas[player].Bounds;

        public void StopDrawing(Player player) => playerAreas[player].StopDrawing();

        public bool Drawing(Player player)
        {
            return playerAreas[player].Drawing;
        }

        public Trampoline TrampolineOf(Player player) => playerAreas[player].Trampoline;
    }
}