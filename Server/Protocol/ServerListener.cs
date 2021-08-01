using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Core.Cards;
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

        public event Action OnUpdate;
        public event Action<ClientWrapper, IPacket> OnHandlePacket; 
        
        public ServerPacketBus HandlerBus { get; protected set; }
        
        public ServerListener(ClientRegistry registry, ServerPacketBus handlerBus)
        {
            _registry = registry;
            HandlerBus = handlerBus;
            HandlerBus.CreateCallbackDriver(300, new ServerCallbackDispatcher());
            
            var evt = new EventBasedNetListener();
            _net = new NetManager(evt);

            evt.ConnectionRequestEvent += HandleConnectionRequest;
            evt.PeerConnectedEvent += HandlePeerConnected;
            evt.PeerDisconnectedEvent += HandlePeerDisconnected;
            evt.NetworkReceiveEvent += HandleNetworkReceive;
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

                OnUpdate?.Invoke();

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
            OnHandlePacket?.Invoke(client, packet);
        }
    }
}
