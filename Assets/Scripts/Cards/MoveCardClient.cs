using LiteNetLib.Utils;
using MagicCardGame.Network;
using UnityEngine;

namespace MagicCardGame
{
    public abstract class MoveCardClient : ActionCardClient
    {
        public override int EnergyCost => 0;
        protected abstract Vector3 MoveVector { get; }

        public override void CastCard(INetworkPlayer networkPlayer)
        {
            networkPlayer.SendCardCastToServer(CardId);
            networkPlayer.MoveLocally(MoveVector);
        }
    }

    public class MoveLeftCardClient : MoveCardClient
    {
        public override int CardId => 0;
        public override int EnergyCost => 0;
        protected override Vector3 MoveVector => Vector3.left;
    }
    
    public class MoveRightCardClient : MoveCardClient
    {
        public override int CardId => 1;
        public override int EnergyCost => 0;
        protected override Vector3 MoveVector => Vector3.right;
    }
    
    public class MoveUpCardClient : MoveCardClient
    {
        public override int CardId => 2;
        public override int EnergyCost => 0;
        protected override Vector3 MoveVector => Vector3.up;
    }
}