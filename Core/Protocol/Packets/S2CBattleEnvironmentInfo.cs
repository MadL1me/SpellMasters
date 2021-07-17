using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Core.Player;

namespace Core.Protocol.Packets
{
    public class S2CBattleEnvironmentInfo : S2CPacketBase
    {
        public override ushort PacketId { get; }
        public override bool UseEncryption { get; }

        public BattleEnvironment BattleEnvironment;

        protected override void WriteDataOctets(OctetWriter writer)
        {
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
        }
    }
}