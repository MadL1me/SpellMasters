using Core.Protocol.Packets;

namespace Server
{
    public static class GlobalSettings
    {
        public static Server MainServer;

        public const uint ServerVersion = 1;
        public const uint ProtocolVersion = 1;
        
        public const EncryptionAlgorithm EncryptionAlgorithm = Core.Protocol.Packets.EncryptionAlgorithm.RsaAes;
        public const ushort EncryptionProtocolVersion = 1;
    }
}