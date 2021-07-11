using System;
using Core.Protocol.Packets;

namespace Core.Protocol
{
    public abstract class BasePacket : IPacket
    {
        private static Func<BasePacket>[] _packetRegistry = new Func<BasePacket>[256];

        static BasePacket()
        {
            RegisterPacketType<S2CEncryptionRequest>();
            RegisterPacketType<S2CSymmetricKeyResponse>();
            RegisterPacketType<C2SClientInfo>();
            RegisterPacketType<C2SPublicKeyExchange>();
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