using System;
using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Runtime
{
    public class TrampolineDrawingPanel : MonoBehaviour, DrawTrampolineView
    {
        [field: SerializeField] public LineRenderer Trampoline { get; private set; }

        void Start()
        {
            Trampoline.positionCount = 0;
        }

        public Task Draw(Trampoline trampoline)
        {
            Trampoline.positionCount = 2;
            Trampoline.SetPosition(0, new Vector3(trampoline.Origin.X, trampoline.Origin.Y, 0));
            Trampoline.SetPosition(1, new Vector3(trampoline.End.X, trampoline.End.Y, 0));

            return Task.CompletedTask;
        }

        public Task StopDrawing(Trampoline trampoline)
        {
            Trampoline.positionCount = 0;
            return Task.CompletedTask;
        }
    }
}