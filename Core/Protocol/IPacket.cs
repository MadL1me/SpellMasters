namespace Core.Protocol
{
    public interface IPacket
    {
        ushort PacketId { get; }
        bool UseEncryption { get; }

        void WriteDataOctets(OctetWriter writer);

        byte[] GetDataOctets();
    }
}