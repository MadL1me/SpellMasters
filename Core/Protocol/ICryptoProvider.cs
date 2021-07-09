namespace Core.Protocol
{
    public interface ICryptoProvider
    {
        public byte[] EncryptByteBuffer(byte[] buffer);
        public byte[] DecryptByteBuffer(byte[] buffer);
    }
}