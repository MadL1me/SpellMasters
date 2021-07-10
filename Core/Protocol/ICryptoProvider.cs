namespace Core.Protocol
{
    public interface ICryptoProvider
    {
        byte[] EncryptByteBuffer(byte[] buffer);
        byte[] DecryptByteBuffer(byte[] buffer);
    }
}