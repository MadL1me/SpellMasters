using Core.Player;
using Core.Protocol.Packets;
using Server.Protocol;
using System;
using System.Collections.Generic;

namespace Server.GameLogic
{
    public class Lobby
    {
        public BattleEnvironment Environment { get; protected set; }
        public int LobbySize { get; protected set; }
        public int ConnectedPlayerCount { get; protected set; }
        private bool IsLobbyFull => ConnectedPlayerCount >= LobbySize;

        public Lobby(int lobbySize)
        {
            LobbySize = lobbySize;
            Environment = new BattleEnvironment(LobbySize);
        }

        private bool AddPlayerToPool(ClientWrapper client)
        {
            if (ConnectedPlayerCount >= LobbySize)
                return false;

            Environment.NetworkPlayers[ConnectedPlayerCount++] = new NetworkPlayerServer(client);
            return true;
        }

        private void StartGame(ClientWrapper client)
        {
            Console.WriteLine("Lobby has reached its max capacity. Starting the game...");
            
            client.SendPacket(new S2CBattleEnvironmentInfo {BattleEnvironment = Environment});
        }

        public void LobbyJoinPacketHandler(ClientWrapper client, C2SJoinLobby packet)
        {
            var result = AddPlayerToPool(client);

            if (!result)
            {
                client.RespondWithError(packet, 10001);
                return;
            }

            Console.WriteLine($"Client {client.Id} connected to lobby successfully");

            if (IsLobbyFull)
                StartGame(client);

            client.RespondWithSuccess(packet);
        }
    }
}
