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
        public List<ServerNetworkPlayer> ConnectedPlayers { get; protected set; }
        public int LobbySize { get; protected set; }

        public Lobby(int lobbySize)
        {
            LobbySize = lobbySize;
            ConnectedPlayers = new List<ServerNetworkPlayer>();
        }

        private bool AddPlayerToPool(ClientWrapper client)
        {
            if (ConnectedPlayers.Count >= LobbySize)
                return false;

            var networkPlayer = new ServerNetworkPlayer(client);

            ConnectedPlayers.Add(networkPlayer);

            return true;
        }

        private bool IsLobbyFull => ConnectedPlayers.Count == LobbySize;
        

        private void InitEnvironment()
        {
            if (ConnectedPlayers.Count == 2)
            {
                Environment = new TwoPlayersBattleEnvironment(ConnectedPlayers[0], ConnectedPlayers[1]);
            }
            else
            {
                Environment = new BattleEnvironment(ConnectedPlayers.Count);
                Environment.NetworkPlayers = ConnectedPlayers.ToArray();
            }
        }

        private void StartGame(ClientWrapper client)
        {
            //isn't implemented at this moment
            Console.WriteLine("Lobby has reached its max capacity. Starting the game...");
            
            client.SendPacket(new S2CBattleEnvironmentInfo {BattleEnvironment = Environment});
        }

        public void LobbyJoinPacketHandler(ClientWrapper client, C2SClientInfo clientInfo)
        {
            if(clientInfo.ProtocolVersion != GlobalSettings.ProtocolVersion ||
               clientInfo.ClientVersion != GlobalSettings.ServerVersion)
            {
                client.SendPacket(new S2CWrongVersion());
                return;
            }

            var result = AddPlayerToPool(client);

            if (!result)
            {
                client.SendPacket(new S2CLobbyIsFull());
                return;
            }

            Console.WriteLine($"Client {client.Id} connected to lobby successfully");

            if (IsLobbyFull)
            {
                InitEnvironment();
                StartGame(client);
            }
        }
    }
}
