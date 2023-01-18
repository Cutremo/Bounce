using System;
using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Server.Runtime
{
    public class ServerScoreView : ScoreView
    {
        public async Task SetScore(Score score, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}