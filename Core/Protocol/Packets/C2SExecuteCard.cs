using Core.Cards;

namespace Core.Protocol.Packets
{
    public class C2SExecuteCard : C2SPacketBase
    {
        public override ushort PacketId => 0x0007;
        public override bool UseEncryption => true;
        public ActionCard ActionCard { get; set; }
        public uint HandSlotId { get; set; }
        public uint LobbyId { get; set; }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteUVarInt(HandSlotId);
            writer.WriteUVarInt(ActionCard.NumericId);
            writer.WriteUVarInt(LobbyId);
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            HandSlotId = reader.ReadUVarInt32();
            ActionCard = new ActionCard(reader.ReadUVarInt32());
            LobbyId = reader.ReadUVarInt32();
        }
    }
}