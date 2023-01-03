using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using DG.Tweening;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Runtime
{
    public class BallsView : MonoBehaviour, Application.Runtime.BallsView
    {
        [SerializeField] BallView prefab;
        
        public async Task DropBall(Ball ball)
        {
            var instance = Instantiate(prefab, new Vector3(ball.Position.X, ball.Position.Y, 0), Quaternion.identity);
            var endScale = ball.Diameter;
            
            instance.transform.localScale = Vector3.one * 0.5f * endScale;
            var sequence = DOTween.Sequence();
            sequence.Append(instance.transform.DOScale(Vector3.one * 3 * endScale, 1f))
                .Append(instance.transform.DOScale(Vector3.one * endScale, 0.66f));
            await sequence.AsyncWaitForCompletion();
        }
    }
}