using Server.GameLogic;
using Server.Protocol;
using System.Collections.Generic;

namespace Server
{
    public class Server
    {
        public int Port { get; set; }
        public Lobby MainLobby { get; protected set; }
        public List<Lobby> Lobbies { get; protected set; }
        public ServerListener Listener { get; protected set; }

        public Server(int maxClients, int port)
        {
            Port = port;
            Lobbies = new List<Lobby>();
            MainLobby = new Lobby(2);

            GlobalSettings.MainServer = this; //first class to be created occupies this slot

            ClientRegistry registry = new ClientRegistry(maxClients);
            ServerPacketBus bus = new ServerPacketBus();

            Listener = new ServerListener(registry, bus);
        }

        public void Start()
        {
            Listener.Listen(Port);
        }
    }
}
