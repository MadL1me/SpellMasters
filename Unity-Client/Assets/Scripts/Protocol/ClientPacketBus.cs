using System;
using System.Linq;
using Core.Protocol;
using Core.Protocol.Packets;
using MagicCardGame.Assets.Scripts.GameLogic;
using UnityEngine;

namespace MagicCardGame.Assets.Scripts.Protocol
{
    public class ClientPacketBus : PacketHandlerBus<ServerConnection>
    {
        public ClientPacketBus()
        {
            RegisterHandler(new SimplePacketHandler<ServerConnection, S2CEncryptionRequest>((connection, packet) =>
            {
                connection.SendPacket(new C2SPublicKeyExchange
                {
                    Key = connection.GenerateRSAKeys()
                });
            }));
            
            RegisterHandler(new SimplePacketHandler<ServerConnection, S2CSymmetricKeyResponse>((connection, packet) =>
            {
                var aesKey = connection.DecryptRSABytes(packet.RsaEncryptedAesKey);
                
                var aes = new AESCryptoProvider(aesKey);
                connection.Encryption = aes;
                
                connection.SendPacket(new C2SClientInfo
                {
                    DeviceId = new byte[16]
                });
            }));
            
            // This handler automatically creates a clientside battle env and loads the appropriate scene
            // when a packet is received
            RegisterHandler(new SimplePacketHandler<ServerConnection, S2CBattleEnvironmentInfo>((connection, packet) =>
            {
                BattleEnvironmentClient.CreateAndLoadScene(packet.BattleEnvironment);
            }));
        }
    }
}