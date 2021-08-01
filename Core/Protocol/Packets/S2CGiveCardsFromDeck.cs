using System;
using Core.Cards;

namespace Core.Protocol.Packets
{
    public class S2CGiveCardsFromDeck : S2CPacketBase
    {
        public override ushort PacketId => 0x0008;
        public override bool UseEncryption => true;
        public ActionCard Card { get; set; }

        protected override void ReadDataOctets(OctetReader reader)
        {
            Card = new ActionCard(reader.ReadUVarInt32());
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            Console.WriteLine(Card.NumericId);
            writer.WriteUVarInt(Card.NumericId);
        }
    }
}
