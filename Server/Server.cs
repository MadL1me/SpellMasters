using Server.GameLogic;
using Server.Protocol;
using System.Collections.Generic;

namespace Server
{
    public class Server
    {
        public Lobby MainLobby { get; protected set; }
        public List<Lobby> Lobbies { get; protected set; }
        public ServerListener Listener { get; protected set; }

        public Server(int maxClients, int port)
        {
            Lobbies = new List<Lobby>();
            MainLobby = new Lobby(2);

            GlobalSettings.MainServer = this; //first class to be created occupies this slot

            var registry = new ClientRegistry(maxClients);
            var bus = new ServerPacketBus();

            var server = new ServerListener(registry, bus);
            server.Listen(port);

        }
    }
}
