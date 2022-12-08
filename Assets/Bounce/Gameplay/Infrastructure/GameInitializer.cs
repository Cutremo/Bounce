using System;
using UnityEngine;
using Zenject;

namespace Bounce.Gameplay.Infrastructure.Runtime
{
    public class GameInitializer : MonoBehaviour
    {
        [Inject] readonly Application.Runtime.Gameplay gameplay;

        async void Start()
        {
            await gameplay.Play();
        }
    }
}