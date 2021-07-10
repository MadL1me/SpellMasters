namespace Core.Protocol.Packets
{
    public class C2SPublicKeyExchange : C2SPacketBase
    {
        public override ushort PacketId => 0x0002;
        public override bool UseEncryption => false;

        public ushort EncryptionProtocolVersion { get; set; } = 1;
        public EncryptionAlgorithm EncryptionAlgorithm { get; set; } = EncryptionAlgorithm.RsaAes;
        public byte[] Key { get; set; }

        protected override void WriteDataOctets(OctetWriter writer)
        {
            writer.WriteUVarInt(EncryptionProtocolVersion);
            writer.WriteUVarInt((ushort) EncryptionAlgorithm);
            writer.WriteUVarInt((uint) Key.Length);
            writer.WriteBytes(Key);
        }

        protected override void ReadDataOctets(OctetReader reader)
        {
            EncryptionProtocolVersion = reader.ReadUVarInt16();
            EncryptionAlgorithm = (EncryptionAlgorithm) reader.ReadUVarInt16();
            Key = reader.ReadBytes((int) reader.ReadUVarInt32());
        }
    }
}