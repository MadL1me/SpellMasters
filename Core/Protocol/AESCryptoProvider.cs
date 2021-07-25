using System.IO;
using System.Security.Cryptography;

namespace Core.Protocol
{
    public class AESCryptoProvider : ICryptoProvider
    {
        public byte[] SecretKey { get; }
        public byte[] SecretIV { get; }
        
        private Aes _aes;

        public AESCryptoProvider(byte[] key, byte[] iv)
        {
            _aes = Aes.Create();

            _aes.Padding = PaddingMode.PKCS7;
            _aes.Mode = CipherMode.CBC;
            _aes.Key = key;
            _aes.IV = iv;
            
            SecretKey = _aes.Key;
            SecretIV = _aes.IV;
        }

        public AESCryptoProvider(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                var reader = new OctetReader(ms);

                var key = reader.ReadBytes((int) reader.ReadUVarInt32());
                var iv = reader.ReadBytes((int) reader.ReadUVarInt32());
                
                _aes = Aes.Create();

                _aes.Padding = PaddingMode.PKCS7;
                _aes.Mode = CipherMode.CBC;
                _aes.Key = key;
                _aes.IV = iv;
                
                SecretKey = _aes.Key;
                SecretIV = _aes.IV;
            }
        }

        public AESCryptoProvider()
        {
            _aes = Aes.Create();
            _aes.Padding = PaddingMode.PKCS7;
            _aes.Mode = CipherMode.CBC;

            SecretKey = _aes.Key;
            SecretIV = _aes.IV;
        }

        public byte[] GetData()
        {
            var writer = new OctetWriter();
            
            writer.WriteUVarInt((uint) SecretKey.Length);
            writer.WriteBytes(SecretKey);
            writer.WriteUVarInt((uint) SecretIV.Length);
            writer.WriteBytes(SecretIV);

            return writer.ToArray();
        }
        
        public byte[] EncryptByteBuffer(byte[] buffer)
        {
            using (var ms = new MemoryStream())
            using (var enc = _aes.CreateEncryptor(_aes.Key, _aes.IV))
            using (var crypto = new CryptoStream(ms, enc, CryptoStreamMode.Write))
            {
                crypto.Write(buffer, 0, buffer.Length);
                
                if (!crypto.HasFlushedFinalBlock)
                    crypto.FlushFinalBlock();

                return ms.ToArray();
            }
        }

        public byte[] DecryptByteBuffer(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            using (var dec = _aes.CreateDecryptor(_aes.Key, _aes.IV))
            using (var crypto = new CryptoStream(ms, dec, CryptoStreamMode.Read))
            using (var newMs = new MemoryStream())
            {
                var buf = new byte[64];
                int read;

                do
                {
                    read = crypto.Read(buf, 0, buf.Length);
                    newMs.Write(buf, 0, read);
                } while (read != 0);

                return newMs.ToArray();
            }
        }
    }
}