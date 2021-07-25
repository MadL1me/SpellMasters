using System;
using Core.Protocol.Packets;

namespace Core.Protocol
{
    public abstract class BasePacket : IPacket
    {
        private static Func<BasePacket>[] _packetRegistry = new Func<BasePacket>[256];

        static BasePacket()
        {
            RegisterPacketType<S2CEncryptionRequest>();            // 0x0001
            RegisterPacketType<C2SPublicKeyExchange>();            // 0x0002
            RegisterPacketType<S2CSymmetricKeyResponse>();         // 0x0003
            //RegisterPacketType<S2CWrongVersion>();                // 0x0004
            //RegisterPacketType<S2CLobbyIsFull>();                 // 0x0005
            RegisterPacketType<S2CBattleEnvironmentInfo>();        // 0x0006
            RegisterPacketType<C2SPullCardFromDeck>();             // 0x0007
            RegisterPacketType<S2CGiveCardFromDeck>();             // 0x0008
            RegisterPacketType<C2SJoinLobby>();                    // 0x0009
            RegisterPacketType<C2SClientInfo>();                   // 0x0010
            RegisterPacketType<S2CClientRegistrationConfirm>();    // 0x0011
            
            RegisterPacketType<S2CErrorPacket>();                  // 0x00E0
            RegisterPacketType<C2SErrorPacket>();                  // 0x00E1
        }
        
        public abstract ushort PacketId { get; }
        public abstract bool UseEncryption { get; }
        
        protected BasePacket()
        { }

        protected abstract void WriteDataOctets(OctetWriter writer);
        protected abstract void ReadDataOctets(OctetReader reader);

        public byte[] GetDataOctets()
        {
            var writer = new OctetWriter();
            writer.WriteUInt16(PacketId);
            WriteDataOctets(writer);
            return writer.ToArray();
        }

        public static BasePacket TryReadPacket(OctetReader reader)
        {
            var packetId = reader.ReadUInt16();

            if (packetId >= _packetRegistry.Length)
                return null;
            
            var instFunc = _packetRegistry[packetId];

            if (instFunc == null)
                return null;

            var inst = instFunc();
            inst.ReadDataOctets(reader);

            return inst;
        }

        public static void RegisterPacketType<T>() where T : BasePacket, new()
        {
            var probe = new T();
            var id = probe.PacketId;

            _packetRegistry[id] = () => new T();
        }
    }
}