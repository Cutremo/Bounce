using System.Threading;
using Bounce.Gameplay.Application.Runtime;
using DarkRift.Server;
using Zenject;

namespace Bounce.Server.Runtime
{
    internal class MatchMaking
    {
        readonly MemoryPool<Match> matchPool; //TODO: Create to repository
        readonly CancellationToken cancellationToken;
        Room room = new Room();

        public MatchMaking(IClientManager clientManager, MemoryPool<Match> matchPool, CancellationToken cancellationToken)
        {
            this.matchPool = matchPool;
            this.cancellationToken = cancellationToken;
            clientManager.ClientConnected += Something;
        }

        void Something(object sender, ClientConnectedEventArgs e)
        {
            room.AddClient(e.Client);
            if(room.Full)
                room.Play(matchPool.Spawn(), cancellationToken);
        }
    }
}