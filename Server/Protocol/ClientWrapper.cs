using System;
using System.Linq;
using Core.Protocol;
using Core.Protocol.Packets;
using LiteNetLib;
using Server.GameLogic;

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
        public ICryptoProvider Encryption { get; set; }
        public ServerListener Server { get; }
        
        private ClientRegistry _registry;
        private NetPeer _peer;

        /// <summary>
        /// This field is set up when the player joins the lobby
        /// </summary>
        public NetworkPlayerServer RelatedPlayer { get; set; }

        /// <summary>
        /// This field is set up when the player joins the lobby
        /// </summary>
        public Lobby RelatedLobby { get; set; }

        public ClientWrapper(int id, ServerListener net, ClientRegistry registry, NetPeer peer)
        {
            Id = id;
            State = ClientState.Unencrypted;
            
            Server = net;
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

        /// <summary>
        /// Sends a packet and awaits a callback packet
        /// </summary>
        public void SendPacketWithCallback(
            S2CCallbackPacketBase packet, 
            ServerCallbackDispatcher.CallbackEvent callback,
            ServerCallbackDispatcher.CallbackError errorCallback = null)
        {
            Server.HandlerBus.CallbackDriver
                .RegisterCallback(packet.GetSequenceId(), callback, errorCallback);
            
            SendPacket(packet);
        }
        
        /// <summary>
        /// Responds to a given callback packet with another packet
        /// </summary>
        public void Respond(C2SCallbackPacketBase packet, S2CCallbackPacketBase newPacket)
        {
            newPacket.InheritSequenceId(packet);
            SendPacket(newPacket);
        }
        
        /// <summary>
        /// Responds to a given callback packet with an error
        /// </summary>
        public void RespondWithError(C2SCallbackPacketBase packet, int id)
        {
            var newPacket = new S2CErrorPacket(id);
            newPacket.InheritSequenceId(packet);
            SendPacket(newPacket);
        }
        
        /// <summary>
        /// Responds to a given callback packet with a success
        /// </summary>
        public void RespondWithSuccess(C2SCallbackPacketBase packet)
        {
            RespondWithError(packet, 0);
        }
        
        /// <summary>
        /// Responds to a given callback packet with another packet with callback
        /// </summary>
        public void RespondWithCallback(C2SCallbackPacketBase packet, S2CCallbackPacketBase newPacket,
            ServerCallbackDispatcher.CallbackEvent callback,
            ServerCallbackDispatcher.CallbackError errorCallback = null)
        {
            newPacket.InheritSequenceId(packet);
            SendPacketWithCallback(newPacket, callback, errorCallback);
        }

        public void SendPacket(IPacket packet)
        {
            if (packet.UseEncryption && Encryption == null)
                throw new Exception("Attempted to send an encrypted packet before encryption was established");

            var data = packet.GetDataOctets();
            
            Console.WriteLine("Sending " + string.Join(" ", data.Select(x => x.ToString("X2"))));

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