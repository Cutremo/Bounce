using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using DG.Tweening;
using JunityEngine.DotweenExtensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] SpriteRenderer sprite;
        [SerializeField] ParticleSystem particles;
        [SerializeField] AnimationCurve explossionCurve;
        public async Task ShowAnimation(Ball ball, CancellationToken cancellationToken)
        {
            var endScale = ball.Diameter;
            transform.localScale = Vector3.one * 0.5f * endScale;
            var sequence = DOTween.Sequence(gameObject);
            sequence.Append(transform.DOScale(Vector3.one * 3 * endScale, 1f))
                .Append(transform.DOScale(Vector3.one * endScale, 0.66f));
            await sequence.AsTask(cancellationToken);
        }

        public void MoveTo(Vector3 targetPosition)
        {
            transform.position = targetPosition;
        }

        public Task Pop(CancellationToken ct)
        {
            var sequence = DOTween.Sequence();
            return sequence.Append(sprite.transform.DOScale(Vector3.one * 2.5f, 0.5f).SetEase(explossionCurve))
                .AppendCallback(() => sprite.gameObject.SetActive(false))
                .AppendCallback(() => particles.Play())
                .AppendInterval(particles.main.duration)
                .AppendCallback(() => Destroy(gameObject))
                .AsTask(ct);
        }
    }
}