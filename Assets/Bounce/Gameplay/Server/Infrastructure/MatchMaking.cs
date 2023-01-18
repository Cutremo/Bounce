using DarkRift.Server;

namespace Bounce.Server.Runtime
{
    internal class MatchMaking
    {
        Room room = new Room();

        public MatchMaking(IClientManager clientManager)
        {
            clientManager.ClientConnected += Something;
        }

        void Something(object sender, ClientConnectedEventArgs e)
        {
            room.AddClient(e.Client);
        }
    }
}