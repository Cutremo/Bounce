using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public interface ScoreView
    {
        public Task SetScore(Score score, CancellationToken ct);
    }
}