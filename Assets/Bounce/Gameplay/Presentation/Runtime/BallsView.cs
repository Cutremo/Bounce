using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using DG.Tweening;
using JunityEngine.DotweenExtensions;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Runtime
{
    public class BallsView : MonoBehaviour, Application.Runtime.BallsView
    {
        [SerializeField] BallView prefab;

        BallView instance;
        public async Task DropBall(Ball ball, CancellationToken ct)
        {
            instance = Instantiate(prefab, new Vector3(ball.Position.X, ball.Position.Y, 0), Quaternion.identity);
            await instance.ShowAnimation(ball, ct);
        }

        public Task MoveBall(Ball ball, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            instance.MoveTo(new Vector3(ball.Position.X, ball.Position.Y, 0));
            return Task.CompletedTask;
        }

        public async Task RemoveBall(CancellationToken ct)
        {
            await instance.Pop(ct);
        }
    }
}