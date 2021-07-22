using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Protocol.Packets
{
    public class S2CGiveCardFromDeck : S2CPacketBase
    {
        public override ushort PacketId => 0x0008;
        public override bool UseEncryption => true;
        public int CardId { get; set; }

        protected override void ReadDataOctets(OctetReader reader)
        {
            CardId = reader.ReadVarInt32();
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteVarInt(CardId);
        }
    }
}
