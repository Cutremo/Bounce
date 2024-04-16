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
        [SerializeField] TrailRenderer trail;
        
        [FormerlySerializedAs("explossionCurve")] [SerializeField] AnimationCurve explosionCurve;
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

        public void ClearTrail()
        {
            trail.Clear();
        }
        public Task Pop(CancellationToken ct, Ball ball)
        {
            var sequence = DOTween.Sequence();
            return sequence.Append(sprite.transform.DOScale(Vector3.one * 4f * ball.TimesMultipliedSpeed, 0.2f).SetEase(explosionCurve))
                .AppendInterval(0.1f)
                .AppendCallback(() => sprite.gameObject.SetActive(false))
                .AppendCallback(() => particles.Play())
                .AppendCallback(() => FindObjectOfType<CameraShake>().Shake(3 * ball.TimesMultipliedSpeed))
                .AppendInterval(particles.main.duration)
                .AppendCallback(() => Destroy(gameObject))
                .AsTask(ct);
        }
    }
}