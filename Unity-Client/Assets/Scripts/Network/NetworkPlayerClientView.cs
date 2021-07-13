using DG.Tweening;
using UnityEngine;

namespace MagicCardGame.Network
{
    public class NetworkPlayerClientView : MonoBehaviour
    {  
        public NetworkPlayerClient NetworkPlayer { get; protected set; }
        [SerializeField] private bool _isLocal;
        
        private void Awake()
        {
            NetworkPlayer = new NetworkPlayerClient(this, _isLocal);
        }

        public void Update()
        {
            NetworkPlayer?.Update();
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