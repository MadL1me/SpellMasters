namespace Core.Protocol.Packets
{
    public class C2SPullCardFromDeck : C2SPacketBase
    {
        public override ushort PacketId => 0x0007;
        public override bool UseEncryption => true;

        protected override void ReadDataOctets(OctetReader reader)
        {
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
        }
    }
}
