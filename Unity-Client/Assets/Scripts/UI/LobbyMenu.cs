using Core.Protocol.Packets;
using MagicCardGame.Assets.Scripts.Protocol;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MagicCardGame
{
    public class LobbyMenu : MonoBehaviour
    {
        public Button UpdateButton;
        public Button CreateButton;
        public GameObject ContentZone;

        public GameObject LobbyButtonPrefab;

        private static void UpdateButtonClicked()
        {
            NetworkProvider.Connection.SendPacket(new C2SRequestAvailableLobbies());
        }

        private static void CreateButtonClicked()
        {
            NetworkProvider.Connection.SendPacket(new C2SCreateLobby {slotCount = 1});
        }

        private static void JoinNthLobby(ulong id)
        {
            NetworkProvider.Connection.SendPacketWithCallback(new C2SJoinLobby {Id = id}, (connection, packet) => { });
        }
        
        
        
        public void AvailableLobbiesPacketHandler(ServerConnection server, S2CAvailableLobbies lobbies)
        {
            foreach (Transform child in ContentZone.transform)
                Destroy(child.gameObject);

            foreach (var lobbyInfo in lobbies.Infos)
            {
                GameObject newLobby = Instantiate(LobbyButtonPrefab, ContentZone.transform);

                var newlobbyButton = newLobby.GetComponent<Button>();
                ulong lobbyID = lobbyInfo.Id;
                newlobbyButton.onClick.AddListener(delegate { JoinNthLobby(lobbyID); });

                var text = newLobby.GetComponentInChildren<TMP_Text>();
                text.SetText($"{lobbyInfo.SlotsOccupied} / {lobbyInfo.SlotCount}");
            }
        }

        private void Start()
        {
            UpdateButton.onClick.AddListener(UpdateButtonClicked);
            CreateButton.onClick.AddListener(CreateButtonClicked);
            NetworkProvider.Connection.SendPacket(new C2SRequestAvailableLobbies());
        }
    }
}