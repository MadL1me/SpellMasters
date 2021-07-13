using System;
using Core.Player;
using MagicCardGame.Network;
using UnityEngine;
using NetworkPlayer = Core.Player.NetworkPlayer;

namespace MagicCardGame.Assets.Scripts.GameLogic
{
    public class BattleEnvironmentClient : MonoBehaviour
    {
        [SerializeField] private NetworkPlayerClientView _localPlayer;
        [SerializeField] private NetworkPlayerClientView _otherPlayer;
        
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
            MakeTestEnvironment(_localPlayer.NetworkPlayer, _otherPlayer.NetworkPlayer);    
        }
        
        public void Update()
        {
            BattleEnvironment?.Update(Time.deltaTime);
        }

        private void MakeTestEnvironment(INetworkPlayer player1, INetworkPlayer player2)
        {
            BattleEnvironment = new TwoPlayersBattleEnvironment(player1, player2);
        }
    }
}