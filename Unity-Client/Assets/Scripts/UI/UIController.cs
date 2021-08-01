using UnityEngine;

namespace MagicCardGame
{
    public class UIController : MonoBehaviour
    {
        public static UIController Hr { get; set; }

        public LobbyMenu MainLobbyMenu;
        public GameObject MainConnectWindow;

        private void Awake()
        {
            if (Hr != null)
                DestroyImmediate(gameObject);
            else
                Hr = this;
        }
    }
}
