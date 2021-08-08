using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Protocol.Packets
{
    public class C2SCastCard : C2SPacketBase
    {
        public override ushort PacketId => 0x0011;

        public override bool UseEncryption => true;

        public uint CardId;

        protected override void ReadDataOctets(OctetReader reader)
        {
            CardId = reader.ReadUVarInt32();
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteUVarInt(CardId);
        }
    }
}
