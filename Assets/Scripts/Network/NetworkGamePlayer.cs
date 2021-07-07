using UnityEngine;

namespace MagicCardGame.Network
{
    public class NetworkGamePlayer : MonoBehaviour, INetworkGamePlayer
    {
        public bool IsLocal => _isLocal;
        
        [SerializeField] private bool _isLocal;
        
        public void Move(int units)
        {
            // Do move locally and send packet to server
        }
    }
}