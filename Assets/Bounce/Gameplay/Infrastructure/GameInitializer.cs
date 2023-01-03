using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Bounce.Gameplay.Infrastructure.Runtime
{
    public class GameInitializer : MonoBehaviour
    {
        [Inject] readonly Application.Runtime.Gameplay gameplay;

        [Inject] CancellationTokenSource cancellationTokenSource;
        async void Start()
        {
            await gameplay.Play(cancellationTokenSource.Token);
        }

        void OnDestroy()
        {
            gameplay.Quit();
        }
    }
}