using Server.GameLogic;
using Server.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            ClientRegistry registry = new ClientRegistry(maxClients);
            ServerPacketBus bus = new ServerPacketBus();

            ServerListener server = new ServerListener(registry, bus);
            server.Listen(port);

        }
    }
}
