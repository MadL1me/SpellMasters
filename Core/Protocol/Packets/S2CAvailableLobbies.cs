using System.Runtime.InteropServices;

namespace Core.Protocol.Packets
{
    public class S2CAvailableLobbies : S2CPacketBase
    {

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct LobbyInfo
        {
            public uint SlotCount;
            public uint SlotsOccupied;
            public ulong Id;
        }

        public override ushort PacketId => 0x000E;
        public override bool UseEncryption => true;

        public int ArraySize;
        public LobbyInfo[] Infos;
        
        protected override void ReadDataOctets(OctetReader reader)
        {
            ArraySize = reader.ReadVarInt32();
            Infos = new LobbyInfo[ArraySize];

            for (int i = 0; i < Infos.Length; i++)
            {
                Infos[i] = new LobbyInfo
                {
                    SlotCount = reader.ReadUVarInt32(),
                    SlotsOccupied = reader.ReadUVarInt32(),
                    Id = reader.ReadUVarInt64()
                };
            }
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteVarInt(ArraySize);

            for(int i = 0; i < Infos.Length; i++)
            {
                writer.WriteUVarInt(Infos[i].SlotCount);
                writer.WriteUVarInt(Infos[i].SlotsOccupied);
                writer.WriteUVarInt(Infos[i].Id);
            }
        }
    }
}
