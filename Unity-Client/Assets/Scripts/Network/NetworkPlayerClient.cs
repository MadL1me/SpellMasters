using Core.Cards;
using Core.Player;
using DG.Tweening;
using LiteNetLib;
using LiteNetLib.Utils;
using UnityEngine;
using NetworkPlayer = Core.Player.NetworkPlayer;

namespace MagicCardGame.Network
{
    public class NetworkPlayerClient : NetworkPlayer
    {
        public bool IsLocal { get; set; }

        private NetPeer _server;
        private NetManager _clientManager;
        private EventBasedNetListener _listener;
        private NetDataWriter _dataWriter;
        private NetworkPlayerStats _playerStats;
        private NetworkPlayerClientView _view;
        
        public NetworkPlayerClient(NetworkPlayerClientView view, bool isLocal)
        {
            IsLocal = isLocal;
            _view = view;
            _listener = new EventBasedNetListener();
            _clientManager = new NetManager(_listener);
            _dataWriter = new NetDataWriter();
        }

        public override void Update()
        {
            //for testing purposes
            if (!IsLocal)
                return;
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                new MoveLeftCardClient(new ActionCardConfig(0, 0)).CastCard(this, null);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                new MoveRightCardClient(new ActionCardConfig(1, 0)).CastCard(this, null);
            if (Input.GetKeyDown(KeyCode.UpArrow))
                new MoveUpCardClient(new ActionCardConfig(2, 0)).CastCard(this, null);
        }

        public override void InitPlayerCharacterFromNetwork()
        {
            
        }

        public override void Move(NetVector2 vector)
        {
            _view.Move(new Vector2(vector.X, vector.Y));
        }

        public override void CastCardAcrossNetwork(int CardId)
        {
            _dataWriter.Put(CardId);
            _server?.Send(_dataWriter, DeliveryMethod.ReliableOrdered);
            _dataWriter.Reset();
        }
    }
}