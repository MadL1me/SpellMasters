using Core.GameLogic;
using Core.Utils;
using Server.Protocol;
using System;
using Core.Entities;

namespace Server.GameLogic
{
    public class NetworkPlayerServer : NetworkedPlayer
    {
        public ClientWrapper BoundClient { get; protected set; }

        public NetworkPlayerServer(ClientWrapper client) : base((uint) (1000000 + client.Id))
        {
            BoundClient = client;
        }
    }
}
