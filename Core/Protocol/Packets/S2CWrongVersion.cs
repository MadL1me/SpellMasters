namespace Core.Protocol.Packets
{
    public class S2CWrongVersion : S2CPacketBase
    {
        public override ushort PacketId => 0x0004;
        public override bool UseEncryption => true;

        protected override void ReadDataOctets(OctetReader reader)
        {
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
        }
    }
}
