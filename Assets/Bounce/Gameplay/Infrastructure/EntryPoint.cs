using System;
using System.Threading;
using Bounce.Gameplay.Application.Runtime;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Bounce.Gameplay.Infrastructure.Runtime
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] readonly Application.Runtime.Gameplay gameplay;
        [Inject] readonly EndGame endGame;
        [Inject] CancellationTokenSource cancellationTokenSource;

        
        async void Start()
        {
            await gameplay.Play(cancellationTokenSource.Token);
        }

        void OnApplicationQuit()
        {
            cancellationTokenSource.Cancel();
            DOTween.KillAll();
        }
    }
}