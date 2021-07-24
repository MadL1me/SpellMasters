namespace Core.Protocol.Packets
{
    public abstract class C2SPacketBase : BasePacket
    {
        public override bool UseEncryption => true;
    }
}