using System.Collections.Generic;
using Core.Cards.Projectiles;
using Core.Collision;
using Core.Entities;
using Core.Utils;

namespace Core.Player
{
    public abstract class BattleEnvironment
    {
        public INetworkPlayer[] NetworkPlayers { get; protected set; }
       
        public NetworkedObject[] NetworkedObjects { get; protected set; }
        
        public List<Projectile> Projectiles { get; protected set; }
        public PhysicsEngine PhysicsEngine { get; protected set; }
        
        public void Update(float deltaTime)
        {
            PhysicsEngine.Update(deltaTime);
        }

        public INetworkPlayer GetClosestCharacter(NetVector2 position)
        {
            var minDistance = float.MaxValue;
            INetworkPlayer closestPlayer = null;
            
            foreach (var networkPlayer in NetworkPlayers)
            {
                var distance = NetVector2.Distance(position, networkPlayer.PlayerCharacter.Position);
                
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    closestPlayer = networkPlayer;
                }
            }

            return closestPlayer;
        }
        
        public INetworkPlayer GetClosestCharacterExcept(NetVector2 position, INetworkPlayer player)
        {
            var minDistance = float.MaxValue;
            INetworkPlayer closestPlayer = null;
            
            foreach (var networkPlayer in NetworkPlayers)
            {
                if (networkPlayer == player)
                    continue;

                var distance = NetVector2.Distance(position, networkPlayer.PlayerCharacter.Position);
                
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    closestPlayer = networkPlayer;
                }
            }

            return closestPlayer;
        }
    }

    public class TwoPlayersBattleEnvironment : BattleEnvironment
    {
        public TwoPlayersBattleEnvironment(INetworkPlayer first, INetworkPlayer second)
        {
            NetworkPlayers = new[]
            {
                first,
                second
            };
        }
    }
}