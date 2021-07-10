using System.Security.Cryptography;

namespace Core.Protocol
{
    public class RSACryptoProvider : ICryptoProvider
    {
        private RSA _rsa;
        private static byte[] _exponent = { 1, 0, 1 };
        
        public byte[] Modulus { get; }

        public RSACryptoProvider(byte[] modulus)
        {
            Modulus = modulus;

            _rsa = RSA.Create();
            
            _rsa.ImportParameters(new RSAParameters
            {
                Modulus = modulus,
                Exponent = _exponent
            });
        }
        
        public RSACryptoProvider()
        {
            _rsa = RSA.Create();
            
            _rsa.ImportParameters(new RSAParameters
            {
                Exponent = _exponent
            });

            var param = _rsa.ExportParameters(false);

            Modulus = param.Modulus;
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