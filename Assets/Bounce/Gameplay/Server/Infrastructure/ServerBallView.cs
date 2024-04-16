using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Server.Runtime
{
    public class ServerBallView : BallsView
    {
        public async Task DropBall(Ball ball, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }

        public async Task MoveBall(Ball position, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }

        public async Task ExplodeBall(Ball ball, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }
    }
}