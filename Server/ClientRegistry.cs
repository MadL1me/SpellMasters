using System.Collections.Generic;
using Core.Protocol;
using LiteNetLib;
using Server.Protocol;

namespace Server
{
    public class ClientRegistry
    {
        public int MaxClientCount { get; set; }
        
        private Dictionary<int, ClientWrapper> _clients;

        public ClientRegistry(int maxClients)
        {
            MaxClientCount = maxClients;
            
            _clients = new Dictionary<int, ClientWrapper>();
        }

        public bool TryRegisterPeer(ServerListener net, NetPeer peer)
        {
            if (_clients.Count >= MaxClientCount)
                return false;

            _clients.Add(peer.Id, new ClientWrapper(peer.Id, net, this, peer));
            _clients[peer.Id].PerformHandshake();
            return true;
        }

        public void KickClient(int id, int errorCode)
        {
            var client = GetClientById(id);
            _clients.Remove(id);
            
            client.DisconnectSocket(errorCode);
        }

        public ClientWrapper GetClientById(int id)
        {
            if (_clients.TryGetValue(id, out var client))
                return client;

            return null;
        }
    }
}