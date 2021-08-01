using System;
using System.Collections.Generic;
using Core.Protocol.Packets;
using Server.Protocol;

namespace Server.GameLogic
{
    public class LobbiesController
    {
        public Dictionary<ulong, Lobby> Lobbies { get; protected set; }
        public ServerListener Listener { get; protected set; }
        public LobbiesServerPacketBus LobbiesServerPacketBus { get; protected set; }
        
        public LobbiesController(ServerListener listener, LobbiesServerPacketBus lobbiesServerPacketBus)
        {
            Listener = listener;
            
            LobbiesServerPacketBus = lobbiesServerPacketBus;
            LobbiesServerPacketBus.CreateCallbackDriver(300, new ServerCallbackDispatcher());

            listener.OnHandlePacket += LobbiesServerPacketBus.HandlePacket;
            listener.OnUpdate += UpdateLobbies;
            
            lobbiesServerPacketBus.OnCreateLobbyRequest += CreateLobbyOnRequestPacketHandler;
            lobbiesServerPacketBus.OnJoinLobbyRequest += LobbyJoinPacketHandler;
            lobbiesServerPacketBus.OnRequestAvailableLobbies += AvailableLobbiesPacketHandler;
            
            InitLobbies();
        }
        
        private void InitLobbies()
        {
            var newLobby = new Lobby(1);
            Lobbies = new Dictionary<ulong, Lobby> {{newLobby.Id, newLobby}};
        }

        private void UpdateLobbies()
        {
            LobbiesServerPacketBus.Update();
            
            foreach(var lobby in Lobbies)
                lobby.Value.Update(15);
        }

        public void CreateLobbyOnRequestPacketHandler(ClientWrapper client, C2SCreateLobby packet)
        {
            var newLobby = new Lobby((int)packet.slotCount);
            Lobbies.Add(newLobby.Id, newLobby);

            AvailableLobbiesPacketHandler(client);
        }

        public void AvailableLobbiesPacketHandler(ClientWrapper client, C2SRequestAvailableLobbies packet = null)
        {
            var lobbiesInfo = new S2CAvailableLobbies { ArraySize = Lobbies.Count };
            lobbiesInfo.Infos = new S2CAvailableLobbies.LobbyInfo[lobbiesInfo.ArraySize];

            var lobbyNumber = 0;
            foreach(var (_, lobbyValue) in Lobbies)
            {
                lobbiesInfo.Infos[lobbyNumber] = new S2CAvailableLobbies.LobbyInfo
                {
                    SlotCount = (uint)lobbyValue.LobbySize,
                    SlotsOccupied = (uint)lobbyValue.ConnectedPlayerCount,
                    Id = lobbyValue.Id
                };

                lobbyNumber++;
            }

            client.SendPacket(lobbiesInfo);
        }

        public void LobbyJoinPacketHandler(ClientWrapper client, C2SJoinLobby packet)
        {
            if (Lobbies.TryGetValue(packet.Id, out var lobby))
            {
                lobby.LobbyJoinPacketHandler(client, packet);
            }
            else
            {
                throw new Exception("SANITY CHECK DEBUG KILL ME");
                client.RespondWithError(packet, 10001);
                return;
            }
        }
    }
}