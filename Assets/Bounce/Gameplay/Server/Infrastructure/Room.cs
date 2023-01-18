using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using DarkRift.Server;

namespace Bounce.Server.Runtime
{
    public class Room
    {
        IList<IClient> players = new List<IClient>();
        public bool Full => players.Count == 2;
        public void AddClient(IClient client)
        {
            if(players.Count == 2)
                throw new Exception();
            
            players.Add(client);
        }

        public Task Play(Match gameplay, CancellationToken ct)
        {
            return gameplay.Play(ct);
        }
    }
}