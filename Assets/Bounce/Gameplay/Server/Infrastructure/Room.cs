using System;
using System.Collections.Generic;
using DarkRift.Server;

namespace Bounce.Server.Runtime
{
    public class Room
    {
        IList<IClient> players = new List<IClient>();
        public void AddClient(IClient client)
        {
            if(players.Count == 2)
                throw new Exception();
            
            players.Add(client);
            if(players.Count == 2)
                BeginGame();
        }

        void BeginGame()
        {
            
        }
    }
}