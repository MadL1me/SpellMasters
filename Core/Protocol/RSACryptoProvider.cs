using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Core.Protocol
{
    public class RSACryptoProvider : ICryptoProvider
    {
        private RSACryptoServiceProvider _rsa;
        private static byte[] _exponent = { 1, 0, 1 };
        
        public RSACryptoProvider(byte[] key, bool isPrivate)
        {
            _rsa = new RSACryptoServiceProvider();

            using (var ms = new MemoryStream(key))
            {
                var reader = new OctetReader(ms);

                if (isPrivate)
                {
                    _rsa.ImportParameters(new RSAParameters
                    {
                        Modulus = reader.ReadBytes((int) reader.ReadUVarInt32()),
                        Exponent = reader.ReadBytes((int) reader.ReadUVarInt32()),
                        D = reader.ReadBytes((int) reader.ReadUVarInt32()),
                        P = reader.ReadBytes((int) reader.ReadUVarInt32()),
                        Q = reader.ReadBytes((int) reader.ReadUVarInt32()),
                        DP = reader.ReadBytes((int) reader.ReadUVarInt32()),
                        DQ = reader.ReadBytes((int) reader.ReadUVarInt32()),
                        InverseQ = reader.ReadBytes((int) reader.ReadUVarInt32())
                    });
                }
                else
                {
                    _rsa.ImportParameters(new RSAParameters
                    {
                        Modulus = reader.ReadBytes((int) reader.ReadUVarInt32()),
                        Exponent = reader.ReadBytes((int) reader.ReadUVarInt32())
                    });
                }
            }
        }

        public byte[] EncryptByteBuffer(byte[] buffer)
        {
            return _rsa.Encrypt(buffer, RSAEncryptionPadding.Pkcs1);
        }

        public byte[] DecryptByteBuffer(byte[] buffer)
        {
            return _rsa.Decrypt(buffer, RSAEncryptionPadding.Pkcs1);
        }

        public static void GenerateKeyPair(uint keyBits, out byte[] publicKey, out byte[] privateKey)
        {
            var rsa = new RSACryptoServiceProvider((int) keyBits);

            var pubData = rsa.ExportParameters(false);
            var pubKeyWriter = new OctetWriter();
            
            pubKeyWriter.WriteUVarInt((uint) pubData.Modulus.Length);
            pubKeyWriter.WriteBytes(pubData.Modulus);
            pubKeyWriter.WriteUVarInt((uint) pubData.Exponent.Length);
            pubKeyWriter.WriteBytes(pubData.Exponent);

            publicKey = pubKeyWriter.ToArray();

            var privData = rsa.ExportParameters(true);
            var privKeyWriter = new OctetWriter();
            
            privKeyWriter.WriteUVarInt((uint) privData.Modulus.Length);
            privKeyWriter.WriteBytes(privData.Modulus);
            privKeyWriter.WriteUVarInt((uint) privData.Exponent.Length);
            privKeyWriter.WriteBytes(privData.Exponent);
            privKeyWriter.WriteUVarInt((uint) privData.D.Length);
            privKeyWriter.WriteBytes(privData.D);
            privKeyWriter.WriteUVarInt((uint) privData.P.Length);
            privKeyWriter.WriteBytes(privData.P);
            privKeyWriter.WriteUVarInt((uint) privData.Q.Length);
            privKeyWriter.WriteBytes(privData.Q);
            privKeyWriter.WriteUVarInt((uint) privData.DP.Length);
            privKeyWriter.WriteBytes(privData.DP);
            privKeyWriter.WriteUVarInt((uint) privData.DQ.Length);
            privKeyWriter.WriteBytes(privData.DQ);
            privKeyWriter.WriteUVarInt((uint) privData.InverseQ.Length);
            privKeyWriter.WriteBytes(privData.InverseQ);
            
            privateKey = privKeyWriter.ToArray();
        }
    }
}