using LiteNetLib.Utils;
using MagicCardGame.Network;
using UnityEngine;

namespace MagicCardGame
{
    public class MoveCardClient : ActionCardClient
    {
        public override int EnergyCost => 0;

        private int _unitsToMove = 5;

        public override void CastCard(INetworkGamePlayer networkGamePlayer)
        {
            networkGamePlayer.Move(_unitsToMove);
        }
    }
}