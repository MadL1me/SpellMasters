using MagicCardGame.Assets.Scripts.Util;
using TMPro;
using UnityEngine;

namespace MagicCardGame.Network
{
    public class NetworkPlayerClientView : MonoBehaviour
    {
        public NetworkPlayerClient NetworkPlayer { get; set; }
        
        [SerializeField] private bool _isLocal;
        [SerializeField] private TMP_Text _displayNameText;
        [SerializeField] private RectTransform _healthBar;
        [SerializeField] private RectTransform _staminaBar;

        public void Update()
        {
            NetworkPlayer.Update();
            
            var healthBarRect = _healthBar.rect;
            var staminaBarRect = _staminaBar.rect;
            healthBarRect.width = 100 * (NetworkPlayer.Health / (float) NetworkPlayer.MaxHealth);
            staminaBarRect.width = 100 * (NetworkPlayer.Energy / (float) NetworkPlayer.MaxEnergy);

            if (_displayNameText.text != NetworkPlayer.DisplayedName)
                _displayNameText.text = NetworkPlayer.DisplayedName;

            // TODO Interpolate this
            transform.position = NetworkPlayer.Position.AsVector2();
        }

        public void MarkAsLocal()
        {
            _isLocal = true;
        }
    }
}