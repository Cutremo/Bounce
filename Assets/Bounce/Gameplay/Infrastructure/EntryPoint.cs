using System;
using System.Threading;
using Bounce.Gameplay.Application.Runtime;
using UnityEngine;
using Zenject;

namespace Bounce.Gameplay.Infrastructure.Runtime
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] readonly Application.Runtime.Gameplay gameplay;

        [Inject] CancellationTokenSource cancellationTokenSource;

        [Inject] EndGame endGame;
        
        async void Start()
        {
            await gameplay.Play(cancellationTokenSource.Token);
        }

        void OnDestroy()
        {
            endGame.Run();
        }
    }
}