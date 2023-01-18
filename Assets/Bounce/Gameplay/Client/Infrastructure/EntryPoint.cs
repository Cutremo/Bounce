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
        [Inject] readonly Application.Runtime.Match match;
        [Inject] readonly EndGame endGame;

        
        async void Start()
        {
            await match.Play(destroyCancellationToken);
        }

        void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}