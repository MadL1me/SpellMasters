using System;
using System.IO;
using System.Linq;
using System.Threading;
using Core.Protocol;
using LiteNetLib;
using UnityEngine;

namespace MagicCardGame.Assets.Scripts.Protocol
{
    public enum ConnectionState
    {
        Unencrypted,
        Encrypted,
        Authorized,
        Invalid
    }
    
    public class ServerConnection
    {
        private const string ServerAuthKey = "--MCG-Proto--";
        
        public ConnectionState State { get; private set; }
        
        private NetManager _net;
        private NetPeer _server;
        private PacketHandlerBus<ServerConnection> _handlerBus;
        private ICryptoProvider _crypto;

        public ServerConnection(PacketHandlerBus<ServerConnection> handlerBus)
        {
            _handlerBus = handlerBus;
            State = ConnectionState.Unencrypted;
            
            var evt = new EventBasedNetListener();
            _net = new NetManager(evt);
            
            evt.NetworkReceiveEvent += HandleNetworkReceive;
            evt.PeerDisconnectedEvent += (peer, info) => State = ConnectionState.Invalid;
        }

        private void HandleNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod delivery)
        {
            var data = reader.GetRemainingBytes();

            if (_crypto != null)
                data = _crypto.DecryptByteBuffer(data);
            
            Debug.Log("Received " + string.Join(" ", data.Select(x => x.ToString("X2"))));
            
            using var stream = new MemoryStream(data);
            var octetReader = new OctetReader(stream);

            var packet = BasePacket.TryReadPacket(octetReader);

            if (packet == null)
            {
                Console.WriteLine($"Malformed packet of length {data.Length}");
                return;
            }
            
            _handlerBus.HandlePacket(this, packet);
        }

        public bool Connect(string hostName, int port)
        {
            _net.Start();
            _server = _net.Connect(hostName, port, ServerAuthKey);

            return _server != null;
        }
        
        public void SendPacket(IPacket packet)
        {
            if (packet.UseEncryption && _crypto == null)
                throw new Exception("Attempted to send an encrypted packet before encryption was established");

            var data = packet.GetDataOctets();

            if (packet.UseEncryption)
                data = _crypto.EncryptByteBuffer(data);
            
            Debug.Log("Sending " + string.Join(" ", data.Select(x => x.ToString("X2"))));
            
            _server.Send(data, DeliveryMethod.ReliableOrdered);
        }
        
        public void Poll()
        {
            _net.PollEvents();
        }
    }
}