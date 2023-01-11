using System.Collections.Generic;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Pitch
    {
        readonly Field field;
        readonly IDictionary<Player, Area> areas;

        public Pitch(Field field, IDictionary<Player, Area> areas)
        { 
            Require(areas).Not.Null();
            Require(areas).Not.Empty();
            this.field = field;
            this.areas = areas;
        }

        public Vector2 Center => field.Center;
        public Ball Ball => field.Ball;

        public void Draw(Player player, Vector2 position)
        {
            Require(areas.Keys.Contains(player)).True();
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
            foreach(var pairs in areas)
            {
                if(pairs.Value.Trampoline.Completed)
                {
                    pairs.Value.HandleCollision(field.Ball, previousPosition);
                }
            }
        }

        public bool Contains(Area area)
        {
            return field.Contains(area);
        }

        public bool InsideArea(Player player, Vector2 position) => areas[player].InsideBounds(position);

        public Bounds2D AreaBoundsOf(Player player) => areas[player].Bounds;

        public void StopDrawing(Player player) => areas[player].StopDrawing();

        public bool Drawing(Player player)
        {
            return areas[player].Drawing;
        }

        public Trampoline TrampolineOf(Player player) => areas[player].Trampoline;
    }
}