﻿using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Domain.Runtime;

namespace Bounce.Gameplay.Application.Runtime
{
    public interface BallsView
    {
        public Task DropBall(Ball ball, CancellationToken ct);
        Task MoveBall(Ball ball, CancellationToken ct);
        Task RemoveBall(CancellationToken ct);
    }
}