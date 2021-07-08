using System;
using DG.Tweening;
using LiteNetLib;
using LiteNetLib.Utils;
using UnityEngine;

namespace MagicCardGame.Network
{
    public class NetworkPlayer : MonoBehaviour, INetworkPlayer
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

        public void MoveLocally(Vector3 vector)
        {
            transform.DOMove(transform.position + vector, 0.5f);
        }

        public void SendCardCastToServer(int CardId)
        {
            _dataWriter.Put(CardId);
            _server?.Send(_dataWriter, DeliveryMethod.ReliableOrdered);
            _dataWriter.Reset();
        }
    }

    public class NetworkPlayerStats 
    {
        public int Health;
        public int Energy;
    }
    
    public struct NetTransform : INetSerializable
    {
        public float X;
        public float Y;
        public float Z;

        public NetTransform(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public NetTransform(Vector3 position)
        {
            X = position.x;
            Y = position.y;
            Z = position.z;
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(X);
            writer.Put(Y);
            writer.Put(Z);
        }

        public void Deserialize(NetDataReader reader)
        {
            X = reader.GetFloat();
            Y = reader.GetFloat();
            Z = reader.GetFloat();
        }
    }
}