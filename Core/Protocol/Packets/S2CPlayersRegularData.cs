using Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Protocol.Packets
{
    public class S2CPlayersRegularData : S2CPacketBase
    {
        public struct PlayerVolatileData
        {
            public uint NetworkId;
            public long Health;
            public int Energy;
            public NetVector2 Position;
        }

        public override ushort PacketId => 0x0010;

        public override bool UseEncryption => true;

        public int PlayersCount;
        public PlayerVolatileData[] PlayersData;

        protected override void ReadDataOctets(OctetReader reader)
        {
            PlayersCount = reader.ReadVarInt32();
            PlayersData = new PlayerVolatileData[PlayersCount];

            for (int i = 0; i < PlayersData.Length; i++)
            {
                PlayersData[i] = new PlayerVolatileData
                {
                    NetworkId = reader.ReadUVarInt32(),
                    Health = reader.ReadVarInt64(),
                    Energy = reader.ReadVarInt32(),
                    Position = new NetVector2
                    {
                        X = reader.ReadVarFixed32(),
                        Y = reader.ReadVarFixed32()
                    }
                };
            }
        }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteVarInt(PlayersCount);

            for (int i = 0; i < PlayersData.Length; i++)
            {
                writer.WriteUVarInt(PlayersData[i].NetworkId);
                writer.WriteVarInt(PlayersData[i].Health);
                writer.WriteVarInt(PlayersData[i].Energy);

                writer.WriteVarFixed(PlayersData[i].Position.X);
                writer.WriteVarFixed(PlayersData[i].Position.Y);
            }
        }
    }
}
