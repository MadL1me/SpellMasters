using System;
using DG.Tweening;
using UnityEngine;
using NetworkPlayer = Core.Player.NetworkPlayer;

namespace MagicCardGame.Network
{
    public class NetworkPlayerClientView : MonoBehaviour
    {
        private NetworkPlayerClient _networkPlayer;
        
        private void Start()
        {
            _networkPlayer = new NetworkPlayerClient(this, true);
        }

        public void Update()
        {
            _networkPlayer?.Update();
        }

        public void InitPlayerFromNetwork()
        {
            
        }

        public void Move(Vector2 vector)
        {
            transform.DOMove(new Vector2(
                transform.position.x + vector.x, 
                transform.position.y + vector.y),0.5f);
        }
        
    }
}