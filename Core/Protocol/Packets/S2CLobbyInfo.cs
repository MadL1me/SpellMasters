namespace Core.Protocol.Packets
{

    public class S2CLobbyInfo : S2CPacketBase
    {
        public override ushort PacketId => 0x000D;

        public override bool UseEncryption => true;

        public uint slotCount;
        public ulong Id;
        public uint slotsOccupied;

        protected override void ReadDataOctets(OctetReader reader)
        {
            slotCount = reader.ReadUVarInt32();
            Id = reader.ReadUVarInt64();
            slotsOccupied = reader.ReadUVarInt32();
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteUVarInt(slotCount);
            writer.WriteUVarInt(Id);
            writer.WriteUVarInt(slotsOccupied);
        }
    }
}
