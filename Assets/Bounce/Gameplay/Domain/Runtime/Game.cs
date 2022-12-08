using System.Collections.Generic;
using JunityEngine.Maths.Runtime;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace Bounce.Gameplay.Domain.Runtime
{
    public class Game
    {
        readonly IDictionary<Player, Area> areas;
        public bool Playing { get; private set; } = false;

        public Game(IDictionary<Player, Area> areas)
        {
            Require(areas).Not.Null();
            Require(areas).Not.Empty();
            this.areas = areas;
        }

        public void Start()
        {
            Require(Playing).False();
            Playing = true;
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
    }
}