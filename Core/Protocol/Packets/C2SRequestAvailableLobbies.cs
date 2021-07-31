using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Protocol.Packets
{
    public class C2SRequestAvailableLobbies : C2SPacketBase
    {
        public override ushort PacketId => 0x000F;
        public override bool UseEncryption => true;

        protected override void WriteDataOctets(OctetWriter writer)
        {

        }

        protected override void ReadDataOctets(OctetReader reader)
        {

        }
    }
}
