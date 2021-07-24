using Core.Player;
using Core.Protocol.Packets;
using MagicCardGame.Assets.Scripts.Protocol;
using MagicCardGame.Network;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using NetworkPlayer = Core.Player.NetworkPlayer;

namespace MagicCardGame.Assets.Scripts.GameLogic
{
    /// <summary>
    /// Represents a clientside battle environment
    /// </summary>
    public class BattleEnvironmentClient
    {
        /// <summary>
        /// Name of the scene to load when in-game
        /// </summary>
        private const string BattleSceneName = "Scenes/TestSceneIlushkins";
        
        /// <summary>
        /// Currently active environment, may be null if player is not in-game
        /// </summary>
        public static BattleEnvironmentClient Current { get; private set; }
        
        public BattleEnvironment SharedEnvironment { get; private set; }
        public NetworkPlayerClient LocalPlayer => SharedEnvironment.NetworkPlayers[0] as NetworkPlayerClient;
        
        /// <summary>
        /// Initializes clientside battle env from an environment object provided
        /// by the server
        /// </summary>
        public BattleEnvironmentClient(BattleEnvironment sharedEnv)
        {
            SharedEnvironment = sharedEnv;
        }

        public void SetAsCurrent() => Current = this;

        /// <summary>
        /// Creates starting entities such as players and etc.
        /// </summary>
        public void CreateStartingEntities()
        {
            foreach (var player in SharedEnvironment.NetworkPlayers)
            {
                if (player == null)
                    continue;

                Addressables.InstantiateAsync("Assets/Prefabs/PlayerPrefab.prefab").Completed += handle =>
                {
                    var inst = handle.Result;
                    
                    // TODO Should make player local here
                };
            }
        }

        /// <summary>
        /// Immediately creates a clientside battle environment and loads the appropriate scene
        /// </summary>
        public static void CreateAndLoadScene(BattleEnvironment sharedEnv)
        {
            var clientEnv = new BattleEnvironmentClient(sharedEnv);
            clientEnv.SetAsCurrent();

            SceneManager.LoadScene(BattleSceneName);
            
            clientEnv.CreateStartingEntities();
        }
    }
}