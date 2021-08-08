using Core.GameLogic;
using Core.Protocol.Packets;
using Core.Utils;
using Server.Protocol;
using System;
using Core.Cards;
using Server.GameLogic.Cards;

namespace Server.GameLogic
{
    public enum LobbyStatus
    {
        WaitingForPlayers,
        InGame,
        GameWasEnded,
    }

    public class Lobby
    {
        public ulong Id { get; }
        public LobbyStatus Status { get; set; }
        public BattleEnvironmentServer Environment { get; protected set; }
        public int LobbySize { get; protected set; }
        public int ConnectedPlayerCount { get; protected set; }
        private bool IsLobbyFull => ConnectedPlayerCount >= LobbySize;

        public Lobby(int lobbySize)
        {
            Id = IdentificationController<Lobby>.GetNextID();
            Status = LobbyStatus.WaitingForPlayers;
            LobbySize = lobbySize;
            Environment = new BattleEnvironmentServer(LobbySize);
        }

        private bool AddPlayerToPool(ClientWrapper client)
        {
            if (IsLobbyFull)
                return false;

            Environment.NetworkPlayers[ConnectedPlayerCount] = new NetworkPlayerServer(client)
            {
                DisplayedName = "PL" + (ConnectedPlayerCount)
            };
            for (var i = 0;
                i < Environment.NetworkPlayers[ConnectedPlayerCount].CardsQueueController.CardsInHand.Length;
                ++i)
                Environment.NetworkPlayers[ConnectedPlayerCount].CardsQueueController.CardsInHand[i] =
                    new ActionCard(0);
            for (var i = 0; i < 1; ++i)
                Environment.NetworkPlayers[ConnectedPlayerCount].CardsQueueController.NextDropCards
                    .Enqueue(new ActionCard(0));

            //this must be done everytime a player is born anywhere across the code kindgom
            Environment.NetworkPlayers[ConnectedPlayerCount].BindToPhysicalEngine(Environment.PhysicsEngine);

            client.RelatedPlayer = (NetworkPlayerServer)Environment.NetworkPlayers[ConnectedPlayerCount];
            client.RelatedLobby = this;
            
            ConnectedPlayerCount++;
            return true;
        }

        /// <summary>
        /// Sends regular data to all players in the lobby
        /// </summary>
        private void PackVolatilePlayersDataAndSend()
        {
            S2CPlayersRegularData data = new S2CPlayersRegularData();
            data.PlayersCount = ConnectedPlayerCount;
            data.PlayersData = new S2CPlayersRegularData.PlayerVolatileData[data.PlayersCount];

            for(var i = 0; i < Environment.NetworkPlayers.Length; i++)
            {
                data.PlayersData[i] = new S2CPlayersRegularData.PlayerVolatileData
                {
                    NetworkId = Environment.NetworkPlayers[i].NetworkId,
                    Health = Environment.NetworkPlayers[i].Health,
                    Energy = Environment.NetworkPlayers[i].Energy,
                    Position = Environment.NetworkPlayers[i].Position
                };
            }

            foreach (NetworkPlayerServer player in Environment.NetworkPlayers)
                player.BoundClient.SendPacket(data);
        }

        private void StartGame(ClientWrapper client)
        {
            Console.WriteLine("Lobby has reached its max capacity. Starting the game...");
            Status = LobbyStatus.InGame;
            client.SendPacket(new S2CBattleEnvironmentInfo
                {BattleEnvironment = Environment, LocalPlayerNetworkId = (uint) client.Id, LobbyId = (uint) Id});
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

        public void Update(float time)
        {
            if (IsLobbyFull)
            {
                Environment.Update(time);
                PackVolatilePlayersDataAndSend();
            }
        }
    }
}