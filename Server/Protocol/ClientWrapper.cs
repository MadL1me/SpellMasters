using System;
using Core.Protocol;
using Core.Protocol.Packets;
using LiteNetLib;

namespace Server.Protocol
{
    public enum ClientState
    {
        Unencrypted,
        Encrypted,
        Authorized
    }
    
    public class ClientWrapper
    {
        public int Id { get; }
        public ClientState State { get; set; }
        public ICryptoProvider Encryption { get; private set; }
        
        private ServerListener _net;
        private ClientRegistry _registry;
        private NetPeer _peer;

        public ClientWrapper(int id, ServerListener net, ClientRegistry registry, NetPeer peer)
        {
            Id = id;
            State = ClientState.Unencrypted;
            
            _net = net;
            _registry = registry;
            _peer = peer;
        }

        public void PerformHandshake()
        {
            SendPacket(new S2CEncryptionRequest
            {
                EncryptionAlgorithm = GlobalSettings.EncryptionAlgorithm,
                EncryptionProtocolVersion = GlobalSettings.EncryptionProtocolVersion
            });
        }

        public void SendPacket(IPacket packet)
        {
            if (packet.UseEncryption && Encryption == null)
                throw new Exception("Attempted to send an encrypted packet before encryption was established");

            var data = packet.GetDataOctets();

            if (packet.UseEncryption)
                data = Encryption.EncryptByteBuffer(data);
            
            _peer.Send(data, DeliveryMethod.ReliableOrdered);
        }

        public void Disconnect(int errorCode) => _registry.KickClient(Id, errorCode);

        public void DisconnectSocket(int errorCode)
        {
            var octetWriter = new OctetWriter();
            octetWriter.WriteVarInt(errorCode);
            
            _peer.Disconnect(octetWriter.ToArray());
        }
    }
}