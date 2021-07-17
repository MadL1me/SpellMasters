using Core.Player;
using Core.Utils;
using Server.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.GameLogic
{
    public class ServerNetworkPlayer : NetworkPlayer
    {
        public ClientWrapper BindedClient { get; protected set; }

        public ServerNetworkPlayer(ClientWrapper client) : base()
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
