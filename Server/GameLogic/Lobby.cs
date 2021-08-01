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
        public BattleEnvironment Environment { get; protected set; }
        public int LobbySize { get; protected set; }
        public int ConnectedPlayerCount { get; protected set; }
        private bool IsLobbyFull => ConnectedPlayerCount >= LobbySize;

        public Lobby(int lobbySize)
        {
            Id = IdentificationController<Lobby>.GetNextID();
            Status = LobbyStatus.WaitingForPlayers;
            LobbySize = lobbySize;
            Environment = new BattleEnvironment(LobbySize);
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

            ConnectedPlayerCount++;
            return true;
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
            Environment.Update(time);
        }
    }
}