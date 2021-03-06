using Core.Protocol;
using Core.Protocol.Packets;
using MagicCardGame.Assets.Scripts.GameLogic;
using MagicCardGame.Network;
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

                // Send client info and wait for registration confirmation
                connection.SendPacketWithCallback(new C2SClientInfo
                    {
                        DeviceId = new byte[16]
                    },
                    (connection, packet) =>
                    {
                        connection.LocalClientId = ((S2CClientRegistrationConfirm) packet).PlayerNetworkId;

                        UIController.Hr.MainConnectWindow.SetActive(false);
                        UIController.Hr.MainLobbyMenu.gameObject.SetActive(true);
                    });
            }));

            // This handler automatically creates a clientside battle env and loads the appropriate scene
            // when a packet is received
            RegisterHandler(new SimplePacketHandler<ServerConnection, S2CBattleEnvironmentInfo>((connection, packet) =>
            {
                BattleEnvironmentClient.LobbyId = packet.LobbyId;
                // This insanely dumb hack forces players to be recreated as clientside once
                // TODusya Figure out how to improve this
                for (var i = 0; i < packet.BattleEnvironment.NetworkPlayers.Length; i++)
                {
                    var player = packet.BattleEnvironment.NetworkPlayers[i];

                    packet.BattleEnvironment.NetworkPlayers[i] = new NetworkPlayerClient
                        (player.NetworkId, player.NetworkId == connection.LocalClientId)
                        {
                            CardsQueueController = player.CardsQueueController,
                            DisplayedName = player.DisplayedName,
                            Energy = player.Energy,
                            Health = player.Health,
                            MaxEnergy = player.MaxEnergy,
                            MaxHealth = player.MaxHealth,
                            Position = player.Position
                        };
                }

                BattleEnvironmentClient.CreateAndLoadScene(connection, packet.BattleEnvironment);
            }));
            
            RegisterHandler(new SimplePacketHandler<ServerConnection,S2CPlayersRegularData>((connection, packet) =>
            {BattleEnvironmentClient.Current.UpdatePlayersFromServer(packet);
            }));

            RegisterHandler(new SimplePacketHandler<ServerConnection, S2CAvailableLobbies>((connection, packet) =>
            {
                UIController.Hr.MainLobbyMenu.AvailableLobbiesPacketHandler(connection, packet);
            }));
            
            RegisterHandler(new SimplePacketHandler<ServerConnection, S2CGiveCardsFromDeck>((connection, packet) =>
            {
                BattleEnvironmentClient.Current.LocalPlayer.CardsQueueController.NextDropCards.Enqueue(packet.Card);
            }));
        }
    }
}