using System;
using System.IO;
using System.Linq;
using Core.Protocol;
using Core.Protocol.Packets;
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
        public ICryptoProvider Encryption { get; set; }
        
        private NetManager _net;
        private NetPeer _server;
        private PacketHandlerBus<ServerConnection> _handlerBus;

        private byte[] _rsaPublicKey;
        private byte[] _rsaPrivateKey;

        public ServerConnection(PacketHandlerBus<ServerConnection> handlerBus)
        {
            _handlerBus = handlerBus;
            _handlerBus.CreateCallbackDriver(300, new ClientCallbackDispatcher());
            State = ConnectionState.Unencrypted;
            
            var evt = new EventBasedNetListener();
            _net = new NetManager(evt);
            
            evt.NetworkReceiveEvent += HandleNetworkReceive;
            evt.PeerDisconnectedEvent += (peer, info) => State = ConnectionState.Invalid;
        }

        private void HandleNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod delivery)
        {
            var data = reader.GetRemainingBytes();

            if (Encryption != null)
                data = Encryption.DecryptByteBuffer(data);
            
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
        
        /// <summary>
        /// Sends a packet and awaits a callback packet
        /// </summary>
        public void SendPacketWithCallback(
            C2SCallbackPacketBase packet, 
            ClientCallbackDispatcher.CallbackEvent callback,
            ClientCallbackDispatcher.CallbackError errorCallback = null)
        {
            _handlerBus.CallbackDriver
                .RegisterCallback(packet.GetSequenceId(), callback, errorCallback);
            
            SendPacket(packet);
        }

        /// <summary>
        /// Responds to a given callback packet with another packet
        /// </summary>
        public void Respond(S2CCallbackPacketBase packet, C2SCallbackPacketBase newPacket)
        {
            newPacket.InheritSequenceId(packet);
            SendPacket(newPacket);
        }
        
        /// <summary>
        /// Responds to a given callback packet with an error
        /// </summary>
        public void RespondWithError(S2CCallbackPacketBase packet, int id)
        {
            var newPacket = new C2SErrorPacket(id);
            newPacket.InheritSequenceId(packet);
            SendPacket(newPacket);
        }
        
        /// <summary>
        /// Responds to a given callback packet with a success
        /// </summary>
        public void RespondWithSuccess(S2CCallbackPacketBase packet)
        {
            RespondWithError(packet, 0);
        }
        
        /// <summary>
        /// Responds to a given callback packet with another packet with callback
        /// </summary>
        public void RespondWithCallback(S2CCallbackPacketBase packet, C2SCallbackPacketBase newPacket,
            ClientCallbackDispatcher.CallbackEvent callback,
            ClientCallbackDispatcher.CallbackError errorCallback = null)
        {
            newPacket.InheritSequenceId(packet);
            SendPacketWithCallback(newPacket, callback, errorCallback);
        }
        
        public void SendPacket(IPacket packet)
        {
            if (packet.UseEncryption && Encryption == null)
                throw new Exception("Attempted to send an encrypted packet before encryption was established");

            var data = packet.GetDataOctets();

            if (packet.UseEncryption)
                data = Encryption.EncryptByteBuffer(data);
            
            Debug.Log("Sending " + string.Join(" ", data.Select(x => x.ToString("X2"))));
            
            _server.Send(data, DeliveryMethod.ReliableOrdered);
        }
        
        public void Poll()
        {
            _handlerBus.Update();
            _net.PollEvents();
        }

        public byte[] GenerateRSAKeys()
        {
            RSACryptoProvider.GenerateKeyPair(2048, out _rsaPublicKey, out _rsaPrivateKey);
            
            return _rsaPublicKey;
        }

        public byte[] DecryptRSABytes(byte[] bytes)
        {
            var rsa = new RSACryptoProvider(_rsaPrivateKey, true);

            return rsa.DecryptByteBuffer(bytes);
        }
    }
}