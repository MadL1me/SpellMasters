namespace Core.Protocol
{
    public interface IPacketSequenceProvider
    {
        uint GetSequenceId();
        bool IsErrorPacket();
    }
}