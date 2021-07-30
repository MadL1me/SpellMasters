namespace Core.Protocol.Packets
{
    public class C2SClientInfo : C2SCallbackPacketBase
    {
        public override ushort PacketId => 0x000A;
        public override bool UseEncryption => true;

        public uint ClientVersion { get; set; } = 1;
        public uint ProtocolVersion { get; set; } = 1;
        public byte[] DeviceId { get; set; }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            base.WriteDataOctets(writer);
            
            writer.WriteUVarInt(ClientVersion);
            writer.WriteUVarInt(ProtocolVersion);
            writer.WriteBytes(DeviceId);
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            base.ReadDataOctets(reader);
            
            ClientVersion = reader.ReadUVarInt32();
            ProtocolVersion = reader.ReadUVarInt32();
            DeviceId = reader.ReadBytes(16);
        }
    }
}