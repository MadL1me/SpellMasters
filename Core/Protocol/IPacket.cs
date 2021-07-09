namespace Core.Protocol
{
    public interface IPacket
    {
        public ushort PacketId { get; }
        public bool UseEncryption { get; }

        protected void WriteDataOctets(OctetWriter writer);

        public byte[] GetDataOctets()
        {
            var writer = new OctetWriter();
            writer.WriteUInt16(PacketId);
            WriteDataOctets(writer);
            return writer.ToArray();
        }
    }
}