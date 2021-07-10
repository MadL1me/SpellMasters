namespace Core.Protocol
{
    public abstract class BasePacket : IPacket
    {
        public abstract ushort PacketId { get; }
        public abstract bool UseEncryption { get; }

        public abstract void WriteDataOctets(OctetWriter writer);

        public byte[] GetDataOctets()
        {
            var writer = new OctetWriter();
            writer.WriteUInt16(PacketId);
            WriteDataOctets(writer);
            return writer.ToArray();
        }
    }
}