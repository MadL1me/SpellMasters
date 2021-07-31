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
            
            RegisterHandler(new SimplePacketHandler<ClientWrapper, C2SClientInfo>((client, packet) =>
            {
                client.Respond(packet, new S2CClientRegistrationConfirm 
                    { PlayerNetworkId = (uint) (1000000 + client.Id) });
            }));

            RegisterHandler(new SimplePacketHandler<ClientWrapper, C2SCreateLobby>((client, packet) =>
            {
                client.Server.CreateLobbyOnRequestPacketHandler(client, packet);
            }));

            RegisterHandler(new SimplePacketHandler<ClientWrapper, C2SJoinLobby>((client, packet) =>
                client.Server.LobbyJoinPacketHandler(client, packet)));

            RegisterHandler(new SimplePacketHandler<ClientWrapper, C2SRequestAvailableLobbies>((client, packet) =>
                client.Server.AvailableLobbiesPacketHandler(client, packet)));
        }
    }
}