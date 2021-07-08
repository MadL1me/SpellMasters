using System;
using System.Collections.Generic;
using System.Threading;
using Core.Player;
using LiteNetLib;
using LiteNetLib.Utils;
using MagicCardGame.Network;

namespace Server
{
    public class GameSimulationServer
    {
        public bool IsRunning { get; private set; }
        public int Port { get; }
        
        private List<NetPeer> _connectedPeers;
        private NetManager _netManager;
        private EventBasedNetListener _listener;

        private Dictionary<NetPeer, INetworkPlayer> _peerToPlayer = new();
        private Dictionary<INetworkPlayer, NetPeer> _playerToPeer = new();
        
        public GameSimulationServer(int port)
        {
            Port = port;

            _listener = new EventBasedNetListener();
            _netManager = new NetManager(_listener);

            _listener.ConnectionRequestEvent += OnConnectionRequest;
            _listener.PeerConnectedEvent += OnPeerConnected;
        }

        private void OnPeerConnected(NetPeer peer)
        {
            Console.WriteLine("We got connection: {0}", peer.EndPoint);
            var newPlayer = new NetworkPlayerServer();
            _peerToPlayer.Add(peer, newPlayer);
            _playerToPeer.Add(newPlayer, peer);
        }

        private void OnConnectionRequest(ConnectionRequest request)
        {
            request.Accept();
        }
        
        public void Run()
        {
            Console.WriteLine("Server is running...");
            IsRunning = true;
            Update();
        }

        public void Update()
        {
            while (IsRunning)
            {
                _netManager.PollEvents();
                Thread.Sleep(15);
            }
        }
        
        public void Stop()
        {
            IsRunning = false;
        }
    }
}