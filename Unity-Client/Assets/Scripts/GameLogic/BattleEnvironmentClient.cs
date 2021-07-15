using Core.Player;
using MagicCardGame.Network;
using UnityEngine;

namespace MagicCardGame.Assets.Scripts.GameLogic
{
    public class BattleEnvironmentClient : MonoBehaviour
    {
        [SerializeField] private NetworkPlayerClientView _localPlayer;
        [SerializeField] private NetworkPlayerClientView _otherPlayer;
        [SerializeField] private CardUIHandler _cardUIHandler;
        
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
            _cardUIHandler.MainHolder.Environment = BattleEnvironment;
            _cardUIHandler.MainHolder.networkPlayer = _localPlayer.NetworkPlayer;
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