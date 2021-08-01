using System.Collections.Generic;
using System.Linq;
using Core.Cards.Projectiles;
using Core.Collision;
using Core.Entities;
using Core.Utils;

namespace Core.GameLogic
{
    public enum BattleEnvironmentState
    {
        NotStarted,
        InGame,
        Finished
    }
    
    /// <summary>
    /// Represents main controller or gameloop for gameplay logic. It runs and must have unique instance
    /// for each players match.
    /// <para>&#160;</para>
    /// Includes: physics, projectiles, effects on player, etc.
    /// </summary>
    public class BattleEnvironment
    {
        public NetworkedPlayer[] NetworkPlayers { get; set; }
       
        public List<Projectile> Projectiles { get; protected set; }
        public PhysicsEngine PhysicsEngine { get; protected set; } = new PhysicsEngine();
        public Dictionary<MobNetworkedEntity, EntityEffectsController> EffectsControllers { get; protected set; } =
            new Dictionary<MobNetworkedEntity, EntityEffectsController>();

        public BattleEnvironmentState State { get; protected set; }
        
        public BattleEnvironment(int lobbySize)
        {
            NetworkPlayers = new NetworkedPlayer[lobbySize];
            Start();
        }

        /// <summary>
        /// TODO: Make start only if lobby is full
        /// </summary>
        public void Start()
        {            
            State = BattleEnvironmentState.InGame;
        }
        
        public void Update(float deltaTime)
        {
            PhysicsEngine.Update(deltaTime);
            
            foreach (var entityEffectsController in EffectsControllers)
                entityEffectsController.Value.UpdateEffects(deltaTime);
            
            CheckForWinner();
        }

        /// <summary>
        /// Currently checks for 2 players duel, so i suppose we need two more battle environments soon:
        /// DuelBattleEnv and TeamBattleEnv to check this stuff differently
        /// </summary>
        private void CheckForWinner()
        {
            if (NetworkPlayers.Count(player => player.IsDead) != NetworkPlayers.Length-1)
                return;
            
            State = BattleEnvironmentState.Finished;
        }
        
        /// <summary>
        /// Gets guy who hasn't died if match ended
        /// </summary>
        /// <returns></returns>
        public NetworkedPlayer GetWinner()
        {
            if (State != BattleEnvironmentState.Finished)
                return null;

            return NetworkPlayers.Single(player => !player.IsDead);
        }

        public void MovePlayer(NetworkedPlayer player)
        {
            
        }

        public NetworkedPlayer GetClosestCharacter(NetVector2 position)
        {
            var minDistance = float.MaxValue;
            NetworkedPlayer closestPlayer = null;
            
            foreach (var networkPlayer in NetworkPlayers)
            {
                var distance = NetVector2.Distance(position, networkPlayer.Position);
                
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    closestPlayer = networkPlayer;
                }
            }

            return closestPlayer;
        }
        
        public NetworkedPlayer GetClosestCharacterExcept(NetVector2 position, NetworkedPlayer player)
        {
            var minDistance = float.MaxValue;
            NetworkedPlayer closestPlayer = null;
            
            foreach (var networkPlayer in NetworkPlayers)
            {
                if (networkPlayer == player)
                    continue;

                var distance = NetVector2.Distance(position, networkPlayer.Position);
                
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    closestPlayer = networkPlayer;
                }
            }

            return closestPlayer;
        }
    }
}