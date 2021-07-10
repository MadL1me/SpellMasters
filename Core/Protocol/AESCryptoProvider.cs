using System.IO;
using System.Security.Cryptography;

namespace Core.Protocol
{
    public class AESCryptoProvider : ICryptoProvider
    {
        public byte[] SecretKey { get; }
        public byte[] SecretIV { get; }
        
        private Aes _aes;
        private ICryptoTransform _dec;
        private ICryptoTransform _enc;

        public AESCryptoProvider(byte[] key, byte[] iv)
        {
            _aes = Aes.Create();

            _aes.Key = key;
            _aes.IV = iv;

            _dec = _aes.CreateDecryptor(_aes.Key, _aes.IV);
            _enc = _aes.CreateEncryptor(_aes.Key, _aes.IV);
        }

        public AESCryptoProvider()
        {
            _aes = Aes.Create();

            SecretKey = _aes.Key;
            SecretIV = _aes.IV;
            
            _dec = _aes.CreateDecryptor(_aes.Key, _aes.IV);
            _enc = _aes.CreateEncryptor(_aes.Key, _aes.IV);
        }
        
        public byte[] EncryptByteBuffer(byte[] buffer)
        {
            using (var ms = new MemoryStream())
            using (var crypto = new CryptoStream(ms, _enc, CryptoStreamMode.Write))
            {
                crypto.Write(buffer, 0, buffer.Length);

                return ms.ToArray();
            }
        }

        public byte[] DecryptByteBuffer(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            using (var crypto = new CryptoStream(ms, _dec, CryptoStreamMode.Read))
            using (var newMs = new MemoryStream())
            {
                crypto.CopyTo(newMs);

                return newMs.ToArray();
            }
        }
    }
}