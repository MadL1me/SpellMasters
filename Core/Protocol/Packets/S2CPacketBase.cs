namespace Core.Protocol.Packets
{
    public abstract class S2CPacketBase : BasePacket
    {
        public override bool UseEncryption => true;
    }
}