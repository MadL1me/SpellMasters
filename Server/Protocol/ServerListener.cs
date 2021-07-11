using System;
using System.IO;
using System.Threading;
using Core.Protocol;
using LiteNetLib;

namespace Server.Protocol
{
    public class ServerListener
    {
        private const int MaxConstantPeers = 50;
        private const string ServerAuthKey = "--MCG-Proto--";
        
        private NetManager _net;
        private CancellationTokenSource _token;
        private ClientRegistry _registry;
        private PacketHandlerBus<ClientWrapper> _handlerBus;
        
        public ServerListener(ClientRegistry registry, PacketHandlerBus<ClientWrapper> handlerBus)
        {
            _registry = registry;
            _handlerBus = handlerBus;
            
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
                _net.PollEvents();
                Thread.Sleep(15);
            }

            _net.Stop();
        }

        private void HandleConnectionRequest(ConnectionRequest request)
        {
            if (_net.ConnectedPeersCount < MaxConstantPeers)
            {
                Console.WriteLine($"Accepted {request.RemoteEndPoint}");
                request.AcceptIfKey(ServerAuthKey);
            }
            else
            {
                Console.WriteLine($"Rejected {request.RemoteEndPoint} due to wrong client key");
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
            
        }

        private void HandleNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod delivery)
        {
            var client = _registry.GetClientById(peer.Id);

            if (client == null)
                return;
            
            var data = reader.RawData;
            
            if (client.Encryption != null)
                data = client.Encryption.DecryptByteBuffer(data);
            
            using var stream = new MemoryStream(data);
            var octetReader = new OctetReader(stream);

            var packet = BasePacket.TryReadPacket(octetReader);

            if (packet == null)
            {
                Console.WriteLine($"Malformed packet from {peer.EndPoint} of length {data.Length}");
                return;
            }
            
            _handlerBus.HandlePacket(client, packet);
        }
    }
}