using Core.Player;
using Core.Utils;
using Server.Protocol;
using System;

namespace Server.GameLogic
{
    public class NetworkPlayerServer : NetworkPlayer
    {
        public ClientWrapper BindedClient { get; protected set; }

        public NetworkPlayerServer(ClientWrapper client) : base()
        {
            BindedClient = client;
        }

        public override void CastCardAcrossNetwork(int cardId)
        {
            throw new NotImplementedException();
        }

        public override void InitPlayerCharacterFromNetwork()
        {
            throw new NotImplementedException();
        }

        public override void Move(NetVector2 vector)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
