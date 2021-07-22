using Core.Player;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MagicCardGame.Network
{
    public class NetworkPlayerClientView : MonoBehaviour
    {
        public NetworkPlayerClient NetworkPlayer { get; protected set; }
        public NetworkPlayerStats PlayerStats { get; protected set; }
        [SerializeField] private bool _isLocal;
        [SerializeField] private TMP_Text _displayNameText;
        [SerializeField] private RectTransform _healthBar;
        [SerializeField] private RectTransform _staminaBar;

        private void Awake()
        {
            NetworkPlayer = new NetworkPlayerClient(this, _isLocal);
            PlayerStats = new NetworkPlayerStats();
            _displayNameText.text = PlayerStats.DisplayName;
        }

        public void Update()
        {
            NetworkPlayer?.Update();
            var healthBarRect = _healthBar.rect;
            var staminaBarRect = _staminaBar.rect;
            healthBarRect.width = 100 / PlayerStats.Health;
            staminaBarRect.width = 100 / PlayerStats.Stamina.Available;

        }

        public void InitPlayerFromNetwork()
        {

        }

        public void Move(Vector2 vector)
        {
            transform.DOMove(new Vector2(
                transform.position.x + vector.x,
                transform.position.y + vector.y), 0.5f);
        }
    }
}