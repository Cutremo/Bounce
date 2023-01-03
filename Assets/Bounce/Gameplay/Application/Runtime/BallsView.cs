using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public interface BallsView
    {
        public Task DropBall(Ball ball, CancellationToken cancellationToken);
        void MoveBall(Ball ball);
    }
}