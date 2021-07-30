namespace Core.Protocol.Packets
{
    public class C2SJoinLobby : C2SCallbackPacketBase
    {
        public override ushort PacketId => 0x0009;

        public ulong Id;

        protected override void WriteDataOctets(OctetWriter writer)
        {
            base.WriteDataOctets(writer);

            writer.WriteUVarInt(Id);
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            base.ReadDataOctets(reader);

            Id = reader.ReadUVarInt64();
        }
    }
}