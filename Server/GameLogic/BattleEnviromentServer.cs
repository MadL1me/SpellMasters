using Core.Cards;
using Core.GameLogic;
using Core.Protocol.Packets;
using Server.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.GameLogic.Cards;

namespace Server.GameLogic
{
    public class BattleEnvironmentServer : BattleEnvironment
    {
        public  NetworkPlayerServer GetServerPlayer(int i) => (NetworkPlayerServer)NetworkPlayers[i];

        public BattleEnvironmentServer(int lobbySize) : base(lobbySize)
        {
            NetworkPlayers = new NetworkPlayerServer[lobbySize];

            new CardBehaviourServer(); // load static stuffs
        }

        public void CastCardPacketHandler(ClientWrapper client, C2SCastCard packet)
        {
            ActionCard actionCard = new ActionCard(packet.CardId);
            actionCard.ExecuteCast(this, client.RelatedPlayer);
        }

    }
}
