using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Protocol.Packets
{
    public class S2CLobbyIsFull : S2CPacketBase
    {
        public override ushort PacketId => 0x0005;
        public override bool UseEncryption => true;

        protected override void ReadDataOctets(OctetReader reader)
        {
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
        }
    }
}
