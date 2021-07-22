using Core.Player;
using Core.Protocol.Packets;
using MagicCardGame.Assets.Scripts.Protocol;
using MagicCardGame.Network;
using UnityEngine;
using System;
using NetworkPlayer = Core.Player.NetworkPlayer;

namespace MagicCardGame.Assets.Scripts.GameLogic
{
    public class BattleEnvironmentClient : MonoBehaviour
    {
        [SerializeField]
        public NetworkPlayerClientView LocalPlayer;
        [SerializeField]
        public NetworkPlayerClientView OtherPlayer;
        
        public static BattleEnvironmentClient Instance { get; private set; }
        public BattleEnvironment BattleEnvironment { get; private set; }

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
                Instance = this;
        }

        public void Start()
        {
            MakeTestEnvironment(LocalPlayer.NetworkPlayer, OtherPlayer.NetworkPlayer);
        }
        
        public void Update()
        {
            BattleEnvironment?.Update(Time.deltaTime);
        }

        private void MakeTestEnvironment(NetworkPlayer player1, NetworkPlayer player2)
        {
            BattleEnvironment = new TwoPlayersBattleEnvironment(player1, player2);
        }

        public void InitFromNetwork(ServerConnection connection, S2CBattleEnvironmentInfo envirInfo)
        {
            if (envirInfo.BattleEnvironment.NetworkPlayers.Length != 2)
                throw new NotImplementedException("players counts different from 2 isn't supported");

            BattleEnvironment = envirInfo.BattleEnvironment;
        }
    }
}