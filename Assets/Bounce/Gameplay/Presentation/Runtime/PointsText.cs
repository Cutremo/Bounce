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
        string PlayerPointsString => game.PointsOf(player).ToString();

        void Start()
        {
            var pos = game.AreaBoundsOf(player).Center;
            transform.position = new Vector3(pos.X, pos.Y, 0);
        }

        public async Task UpdatePoints(CancellationToken ct)
        { 
            if (text.text == PlayerPointsString)
                return;

            await Task.Delay(500, ct);
            text.text = PlayerPointsString;
        }
    }
}