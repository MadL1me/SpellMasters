using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Core.Protocol;
using Core.Protocol.Packets;
using LiteNetLib;
using Server.GameLogic;

namespace Server.Protocol
{
    public class ServerListener
    {
        private const int MaxConstantPeers = 50;
        private const string ServerAuthKey = "--MCG-Proto--";
        
        private NetManager _net;
        private CancellationTokenSource _token;
        private ClientRegistry _registry;

        public ServerPacketBus HandlerBus { get; protected set; }
        
        public Dictionary<ulong, Lobby> Lobbies { get; protected set; }
        
        public ServerListener(ClientRegistry registry, ServerPacketBus handlerBus)
        {
            _registry = registry;
            HandlerBus = handlerBus;
            HandlerBus.CreateCallbackDriver(300, new ServerCallbackDispatcher());

            InitLobbies();
            
            var evt = new EventBasedNetListener();
            _net = new NetManager(evt);

            evt.ConnectionRequestEvent += HandleConnectionRequest;
            evt.PeerConnectedEvent += HandlePeerConnected;
            evt.PeerDisconnectedEvent += HandlePeerDisconnected;
            evt.NetworkReceiveEvent += HandleNetworkReceive;
        }

        private void InitLobbies()
        {
            Lobbies = new Dictionary<ulong, Lobby>();
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

        public void Halt() => _token.Cancel();

        public void Listen(int port)
        {
            _token = new CancellationTokenSource();
            
            _net.Start(port);

            while (!_token.IsCancellationRequested)
            {
                HandlerBus.Update();
                _net.PollEvents();

                foreach(var lobby in Lobbies)
                    lobby.Value.Update(15);

                Thread.Sleep(15);
            }

            _net.Stop();
        }

        private void HandleConnectionRequest(ConnectionRequest request)
        {
            if (_net.ConnectedPeersCount < MaxConstantPeers)
            {
                if (request.AcceptIfKey(ServerAuthKey) != null)
                    Console.WriteLine($"Accepted {request.RemoteEndPoint}");
                else
                    Console.WriteLine($"Rejected {request.RemoteEndPoint} due to wrong client key");
            }
            else
            {
                Console.WriteLine($"Rejected {request.RemoteEndPoint} due to max connected peers reached");
                request.Reject();
            }
        }

        private void HandlePeerConnected(NetPeer peer)
        {
            if (!_registry.TryRegisterPeer(this, peer))
                peer.Disconnect();
        }

        private void HandlePeerDisconnected(NetPeer peer, DisconnectInfo info)
        {
            _registry.TryDeregisterClient(peer.Id);
        }

        private void HandleNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod delivery)
        {
            var client = _registry.GetClientById(peer.Id);

            if (client == null)
                return;

            var data = reader.GetRemainingBytes();
            
            if (client.Encryption != null)
                data = client.Encryption.DecryptByteBuffer(data);

            Console.WriteLine("Received " + string.Join(" ", data.Select(x => x.ToString("X2"))));
            
            using var stream = new MemoryStream(data);
            var octetReader = new OctetReader(stream);

            var packet = BasePacket.TryReadPacket(octetReader);

            if (packet == null)
            {
                Console.WriteLine($"Malformed packet from {peer.EndPoint} of length {data.Length}");
                return;
            }
            
            HandlerBus.HandlePacket(client, packet);
        }
    }
}