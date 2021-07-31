using Core.Protocol.Packets;
using MagicCardGame.Assets.Scripts.Protocol;
using System.Collections;
using System.Collections.Generic;
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

        private void UpdateButtonClicked()
        {
            foreach (Transform child in ContentZone.transform)
            {
                Destroy(child.gameObject);
            }
            NetworkProvider.Connection.SendPacket(new C2SRequestAvailableLobbies());
        }

        private void CreateButtonClicked()
        {
            NetworkProvider.Connection.SendPacket(new C2SCreateLobby { slotCount = 1 });
        }

        void JoinNthLobby(ulong id)
        {
            NetworkProvider.Connection.SendPacketWithCallback(new C2SJoinLobby { Id = id }, (connection, packet) => { });
        }

        public void AvailableLobbiesPacketHandler(ServerConnection server, S2CAvailableLobbies lobbies)
        {

            for (int i = 0; i < lobbies.Infos.Length; i++)
            {
                GameObject newLobby = Instantiate(LobbyButtonPrefab);

                Button newlobbyButton = newLobby.GetComponent<Button>();
                ulong lobbyID = lobbies.Infos[i].Id;
                newlobbyButton.onClick.AddListener(delegate { JoinNthLobby(lobbyID); });

                TMP_Text text = newLobby.GetComponentInChildren<TMP_Text>();
                text.SetText($"0 / {lobbies.Infos[i].SlotCount}");

                newLobby.transform.SetParent(ContentZone.transform);
            }
        }
        void Start()
        {
            UpdateButton.onClick.AddListener(UpdateButtonClicked);
            CreateButton.onClick.AddListener(CreateButtonClicked);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
