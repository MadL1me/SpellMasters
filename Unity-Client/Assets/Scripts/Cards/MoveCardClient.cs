using Core.Cards;
using Core.Player;
using LiteNetLib.Utils;
using MagicCardGame.Network;
using UnityEngine;

namespace MagicCardGame
{
    public class MoveLeftCardClient : MoveLeftCard
    {
        public override void CastCard(INetworkPlayer networkPlayer)
        {
            networkPlayer.Move(MoveVector);
        }
    }
    
    public class MoveRightCardClient : MoveRightCard
    {
         public override void CastCard(INetworkPlayer networkPlayer)
         {
             networkPlayer.Move(MoveVector);
         }
    }
    
    public class MoveUpCardClient : MoveUpCard
    {
        public override void CastCard(INetworkPlayer networkPlayer)
        {
            networkPlayer.Move(MoveVector);
        }
    }
}