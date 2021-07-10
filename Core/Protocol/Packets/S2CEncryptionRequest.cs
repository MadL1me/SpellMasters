namespace Core.Protocol.Packets
{
    public enum EncryptionAlgorithm : ushort
    {
        NoEncryption = 0,
        RsaAes = 1
    }
    
    public class S2CEncryptionRequest : S2CPacketBase
    {
        public override ushort PacketId => 0x0001;
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