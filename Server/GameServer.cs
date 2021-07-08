using System;
using System.Collections.Generic;
using System.Threading;
using LiteNetLib;
using LiteNetLib.Utils;

namespace Server
{
    public class GameServer
    {
        public bool IsRunning { get; private set; }
        public int Port { get; }
        
        private List<NetPeer> _connectedPeers;
        private NetManager _netManager;
        private EventBasedNetListener _listener;

        public GameServer(int port)
        {
            Port = port;

            _listener = new EventBasedNetListener();
            _netManager = new NetManager(_listener);
            _netManager.Start(port);

            _listener.ConnectionRequestEvent += OnConnectionRequest;
            _listener.PeerConnectedEvent += OnPeerConnected;
        }

        private void OnPeerConnected(NetPeer peer)
        {
            Console.WriteLine("We got connection: {0}", peer.EndPoint);
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