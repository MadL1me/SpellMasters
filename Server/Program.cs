using System;
using System.IO;
using System.Linq;
using System.Text;
using Core.Protocol;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Encoding.ASCII.GetBytes("Hello world");

            var aes = new AESCryptoProvider();
            
            RSACryptoProvider.GenerateKeyPair(2048, out var publicKey, out var privateKey);
            
            var serverRsa = new RSACryptoProvider(privateKey, true);

            var clientRsa = new RSACryptoProvider(publicKey, false);
            var encData = clientRsa.EncryptByteBuffer(data);

            var unencData = serverRsa.DecryptByteBuffer(encData);

            Console.WriteLine(Encoding.ASCII.GetString(unencData));
        }
    }
}