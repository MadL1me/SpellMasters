using System;
using Core.Cards;
using Core.Player;
using DG.Tweening;
using LiteNetLib;
using LiteNetLib.Utils;
using UnityEngine;

namespace MagicCardGame.Network
{
    public class NetworkPlayerClient : MonoBehaviour, INetworkPlayer
    {
        public bool IsLocal => _isLocal;

        [SerializeField] private bool _isLocal;

        private NetPeer _server;
        private NetManager _clientManager;
        private EventBasedNetListener _listener;
        private NetDataWriter _dataWriter;
        private NetworkPlayerStats _playerStats;
        
        public void Start()
        {
           _listener = new EventBasedNetListener();
           _clientManager = new NetManager(_listener);
           _dataWriter = new NetDataWriter();
        }

        public void MoveCards()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            //for testing purposes
            if (!_isLocal)
                return;
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                new MoveLeftCardClient().CastCard(this);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                new MoveRightCardClient().CastCard(this);
            if (Input.GetKeyDown(KeyCode.UpArrow))
                new MoveUpCardClient().CastCard(this);
        }
        
        public void Move(NetVector2 vector)
        {
            var unityVector = new Vector2(vector.X, vector.Y);
            transform.DOMove(new Vector2(
                transform.position.x + unityVector.x, 
                transform.position.y + unityVector.y),0.5f);
        }

        public void SendCardCastToServer(int CardId)
        {
            _dataWriter.Put(CardId);
            _server?.Send(_dataWriter, DeliveryMethod.ReliableOrdered);
            _dataWriter.Reset();
        }
    }
}