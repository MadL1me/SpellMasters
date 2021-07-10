namespace Core.Protocol.Packets
{
    public class S2CSymmetricKeyResponse : S2CPacketBase
    {
        public override ushort PacketId => 0x0003;
        public override bool UseEncryption => false;

        public ushort EncryptionProtocolVersion { get; set; } = 1;
        public EncryptionAlgorithm EncryptionAlgorithm { get; set; } = EncryptionAlgorithm.RsaAes;
        public byte[] RsaEncryptedAesKey { get; set; }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteUVarInt(EncryptionProtocolVersion);
            writer.WriteUVarInt((ushort) EncryptionAlgorithm);
            writer.WriteUVarInt((uint) RsaEncryptedAesKey.Length);
            writer.WriteBytes(RsaEncryptedAesKey);
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            EncryptionProtocolVersion = reader.ReadUVarInt16();
            EncryptionAlgorithm = (EncryptionAlgorithm) reader.ReadUVarInt16();
            RsaEncryptedAesKey = reader.ReadBytes((int) reader.ReadUVarInt32());
        }
    }
}