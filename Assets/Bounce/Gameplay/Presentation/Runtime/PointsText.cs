using System;
using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using TMPro;
using UnityEngine;
using Zenject;

namespace Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime
{
    public class PointsText : MonoBehaviour
    {
        [SerializeField]
        TMP_Text text;

        [Inject] Game game;
        [Inject] Player player;

        void Start()
        {
            var pos = game.AreaBoundsOf(player).Center;
            transform.position = new Vector3(pos.X, pos.Y, 0);
        }

        public Task UpdatePoints(CancellationToken ct)
        {
            text.text = game.PointsOf(player).ToString();
            return Task.CompletedTask;
        }
    }
}