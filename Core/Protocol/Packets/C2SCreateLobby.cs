using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Protocol.Packets
{
    public class C2SCreateLobby : C2SPacketBase
    {
        public override ushort PacketId => 0x000C;

        public override bool UseEncryption => true;

        public uint slotCount;

        protected override void ReadDataOctets(OctetReader reader)
        {
            slotCount = reader.ReadUVarInt32();
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteUVarInt(slotCount);
        }
    }
}
