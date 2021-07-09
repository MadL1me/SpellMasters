using System.Security.Cryptography;

namespace Core.Protocol
{
    public class RSACryptoProvider : ICryptoProvider
    {
        private RSA _rsa;
        
        public byte[] PublicKey { get; }

        public RSACryptoProvider(byte[] publicKey)
        {
            PublicKey = publicKey;

            _rsa = RSA.Create(2048);
            
            _rsa.ImportRSAPublicKey(PublicKey, out _);
        }
        
        public RSACryptoProvider()
        {
            _rsa = RSA.Create(2048);

            PublicKey = _rsa.ExportRSAPublicKey();
        }

        public byte[] EncryptByteBuffer(byte[] buffer)
        {
            return _rsa.Encrypt(buffer, RSAEncryptionPadding.Pkcs1);
        }

        public byte[] DecryptByteBuffer(byte[] buffer)
        {
            return _rsa.Decrypt(buffer, RSAEncryptionPadding.Pkcs1);
        }
    }
}