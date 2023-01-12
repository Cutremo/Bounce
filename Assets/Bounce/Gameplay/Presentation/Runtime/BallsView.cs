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
        public async Task DropBall(Ball ball, CancellationToken cancellationToken)
        {
            instance = Instantiate(prefab, new Vector3(ball.Position.X, ball.Position.Y, 0), Quaternion.identity);
            await instance.ShowAnimation(ball, cancellationToken);
        }

        public void MoveBall(Ball ball)
        {
            instance.MoveTo(new Vector3(ball.Position.X, ball.Position.Y, 0));
        }

        public async Task RemoveBall(CancellationToken cancellationToken)
        {
            Destroy(instance.gameObject);
        }
    }
}