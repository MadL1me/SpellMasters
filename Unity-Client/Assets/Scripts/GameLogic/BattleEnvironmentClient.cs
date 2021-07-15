using Core.Player;
using MagicCardGame.Network;
using UnityEngine;

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

        private void MakeTestEnvironment(INetworkPlayer player1, INetworkPlayer player2)
        {
            BattleEnvironment = new TwoPlayersBattleEnvironment(player1, player2);
        }
    }
}