using System.Collections.Generic;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Pitch
    {
        readonly Field field;
        readonly IDictionary<Player, Area> areas;

        public Pitch(Field field, IDictionary<Player, Area> areas)
        {
            this.field = field;
            this.areas = areas;
        }

        public Vector2 Center => field.Center;
        public Ball Ball => field.Ball;

        public void Draw(Player player, Vector2 position)
        {
            Contract.Require(areas.Keys.Contains(player)).True();
            areas[player].Draw(position);
        }

        public void DropBall(Ball ball)
        {
            field.DropBall(ball);
        }

        public void SimulateBall(float time)
        {
            var previousPosition = field.Ball.Position;
            field.SimulateBall(time);
            foreach(var area in areas)
            {
                area.Value.HandleCollision(field.Ball, previousPosition);
            }
        }

        public bool Contains(Area area)
        {
            return field.Contains(area);
        }
    }
}