using UnityEngine;

namespace MagicCardGame.Network
{
    public interface INetworkPlayer
    {
        bool IsLocal { get; }
        
        public void MoveLocally(Vector3 vector);
       
        public void SendCardCastToServer(int CardId);
    }
}