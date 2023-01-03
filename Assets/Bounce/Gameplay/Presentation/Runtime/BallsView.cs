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
            var endScale = ball.Diameter;
            
            instance.transform.localScale = Vector3.one * 0.5f * endScale;
            var sequence = DOTween.Sequence();
            sequence.Append(instance.transform.DOScale(Vector3.one * 3 * endScale, 1f))
                .Append(instance.transform.DOScale(Vector3.one * endScale, 0.66f));
            await sequence.AsTask(cancellationToken);
        }

        public void MoveBall(Ball ball)
        {
            instance.transform.position = new Vector3(ball.Position.X, ball.Position.Y, 0);
        }
    }
}