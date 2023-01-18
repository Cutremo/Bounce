using System;
using System.Collections;
using System.Collections.Generic;
using Bounce.Gameplay.Domain.Runtime;
using DarkRift.Server;
using UnityEngine;

namespace Bounce.Server.Runtime
{
    public class EntryPoint : Plugin
    {
        Room room = new Room();
        public EntryPoint(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            ClientManager.ClientConnected += ClientConnected;
        }

        void ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            room.AddClient(e.Client);
        }

        public override Version Version => new Version(0, 0, 0);
        public override bool ThreadSafe => true;
    }

    public class Room
    {
        IList<IClient> players = new List<IClient>();
        public void AddClient(IClient client)
        {
            if(players.Count == 2)
                throw new Exception();
            
            players.Add(client);
            if(players.Count == 2)
            {
                BeginGame();
            }
        }

        void BeginGame()
        {
            
        }
    }

}


