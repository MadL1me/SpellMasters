namespace Core.Protocol.Packets
{
    public class C2SClientInfo : S2CPacketBase
    {
        public override ushort PacketId => 0x0010;
        public override bool UseEncryption => false;

        public ushort EncryptionProtocolVersion { get; set; } = 1;
        public EncryptionAlgorithm EncryptionAlgorithm { get; set; } = EncryptionAlgorithm.RsaAes;

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteUVarInt(EncryptionProtocolVersion);
            writer.WriteUVarInt((ushort) EncryptionAlgorithm);
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            EncryptionProtocolVersion = reader.ReadUVarInt16();
            EncryptionAlgorithm = (EncryptionAlgorithm) reader.ReadUVarInt16();
        }
    }
}