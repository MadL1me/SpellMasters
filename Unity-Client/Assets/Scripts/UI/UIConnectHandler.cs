using MagicCardGame.Assets.Scripts.Protocol;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MagicCardGame
{
    public class UIConnectHandler : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _addressInput;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private TMP_Text _errorText;

        private int _requestedConnect;
        private string _hostName;
        private int _port;
        private Vector2 _startPos;
        
        private void Start()
        {
            _startPos = transform.position;
            
            var btn = GetComponent<Button>();
            
            btn.onClick.AddListener(() =>
            {
                if (string.IsNullOrWhiteSpace(_addressInput.text))
                    return;
                
                _progressText.gameObject.SetActive(true);
                _progressText.text = "Connecting...";

                var parts = _addressInput.text.Split(':');
                var port = 9669;

                if (parts.Length != 1)
                    port = int.Parse(parts[1]);

                _hostName = parts[0];
                _port = port;
                
                _errorText.gameObject.SetActive(false);
                _addressInput.gameObject.SetActive(false);
                transform.position = new Vector3(3000, 0);

                _requestedConnect = 2;
            });
        }

        private void Update()
        {
            if (_requestedConnect > 0)
            {
                _requestedConnect--;
                
                if (_requestedConnect != 0)
                    return;
                
                if (!NetworkProvider.Connect(_hostName, _port))
                    ShowError("Failed to connect");
            }
        }

        private void ShowError(string text)
        {
            _progressText.gameObject.SetActive(false);

            _errorText.text = text;
            _errorText.gameObject.SetActive(true);
            
            _addressInput.gameObject.SetActive(true);
            transform.position = _startPos;
        }
    }
}
