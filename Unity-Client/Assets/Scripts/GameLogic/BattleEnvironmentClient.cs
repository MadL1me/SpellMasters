﻿using Core.GameLogic;
using MagicCardGame.Assets.Scripts.Protocol;
using MagicCardGame.Network;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

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
        
        public ServerConnection Server { get; private set; }
        public BattleEnvironment SharedEnvironment { get; private set; }
        public NetworkPlayerClient LocalPlayer { get; private set; }
        
        /// <summary>
        /// Initializes clientside battle env from an environment object provided
        /// by the server
        /// </summary>
        public BattleEnvironmentClient(ServerConnection server, BattleEnvironment sharedEnv)
        {
            Server = server;
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

                var clientPlayer = (NetworkPlayerClient) player;

                var isLocal = clientPlayer.IsLocal;

                if (isLocal)
                    LocalPlayer = clientPlayer;
                
                Addressables.InstantiateAsync("Assets/Prefabs/PlayerPrefab.prefab").Completed += handle =>
                {
                    var view = handle.Result.GetComponent<NetworkPlayerClientView>();
                    view.NetworkPlayer = clientPlayer;
                    
                    if (isLocal)
                        view.MarkAsLocal();
                };
            }
        }

        /// <summary>
        /// Immediately creates a clientside battle environment and loads the appropriate scene
        /// </summary>
        public static void CreateAndLoadScene(ServerConnection server, BattleEnvironment sharedEnv)
        {
            var clientEnv = new BattleEnvironmentClient(server, sharedEnv);
            clientEnv.SetAsCurrent();

            SceneManager.LoadScene(BattleSceneName);
            
            clientEnv.CreateStartingEntities();
        }
    }
}