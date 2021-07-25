using Core.Cards;
using Core.Entities;
using Core.Utils;
using UnityEngine;

namespace MagicCardGame.Network
{
    public class NetworkPlayerClient : NetworkedPlayer
    {
        public bool IsLocal { get; set; }
        
        public NetworkPlayerClient(uint networkId, bool isLocal)
            : base(networkId)
        {
            IsLocal = isLocal;
        }

        public override void Update()
        {
            //for testing purposes
            if (!IsLocal)
                return;
            
            /*if (Input.GetKeyDown(KeyCode.LeftArrow))
                new MoveLeftCardClient(new ActionCardConfig(0, 0), new NetVector2(-1, 0)).CastCard(this, null);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                new MoveRightCardClient(new ActionCardConfig(1, 0), new NetVector2(1, 0)).CastCard(this, null);
            if (Input.GetKeyDown(KeyCode.UpArrow))
                new MoveUpCardClient(new ActionCardConfig(2, 0), new NetVector2(0, 1)).CastCard(this, null);*/
        }
    }
}