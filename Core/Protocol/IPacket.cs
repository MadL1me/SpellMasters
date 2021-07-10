namespace Core.Protocol
{
    public interface IPacket
    {
        ushort PacketId { get; }
        bool UseEncryption { get; }

        byte[] GetDataOctets();
    }
}