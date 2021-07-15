using Core.Protocol;
using Core.Protocol.Packets;

namespace Server.Protocol
{
    public class ServerPacketBus : PacketHandlerBus<ClientWrapper>
    {
        public ServerPacketBus()
        {
            RegisterHandler(new SimplePacketHandler<ClientWrapper, C2SPublicKeyExchange>((client, packet) =>
            {
                var aes = new AESCryptoProvider();
                client.Encryption = aes;
                
                var rsa = new RSACryptoProvider(packet.Key, false);
                var buf = rsa.EncryptByteBuffer(aes.GetData());
                
                client.SendPacket(new S2CSymmetricKeyResponse
                {
                    RsaEncryptedAesKey = buf
                });
            }));
        }
    }
}