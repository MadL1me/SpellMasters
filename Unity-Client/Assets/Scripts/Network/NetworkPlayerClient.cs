using Core.Cards;
using Core.Utils;
using UnityEngine;
using NetworkPlayer = Core.Player.NetworkPlayer;

namespace MagicCardGame.Network
{
    public class NetworkPlayerClient : NetworkPlayer
    {
        public bool IsLocal { get; set; }
        private NetworkPlayerClientView _view;
        
        public NetworkPlayerClient(NetworkPlayerClientView view, bool isLocal)
        {
            IsLocal = isLocal;
            _view = view;
        }

        public override void Update()
        {
            //for testing purposes
            if (!IsLocal)
                return;
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                new MoveLeftCardClient(new ActionCardConfig(0, 0), new NetVector2(-1, 0)).CastCard(this, null);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                new MoveRightCardClient(new ActionCardConfig(1, 0), new NetVector2(1, 0)).CastCard(this, null);
            if (Input.GetKeyDown(KeyCode.UpArrow))
                new MoveUpCardClient(new ActionCardConfig(2, 0), new NetVector2(0, 1)).CastCard(this, null);
        }

        public override void InitPlayerCharacterFromNetwork()
        {
            
        }

        public override void Move(NetVector2 vector)
        {
            _view.Move(new Vector2(vector.X, vector.Y));
        }

        public override void CastCardAcrossNetwork(int cardId)
        {
            // Send packet with new 
        }
    }
}