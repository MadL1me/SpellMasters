using System.Collections.Generic;
using Core.Cards.Projectiles;
using Core.Entities;
using Core.Utils;

namespace Core.Player
{
    public abstract class BattleEnvironment
    {
        public INetworkPlayer[] NetworkPlayers { get; protected set; }
       
        public NetworkedObject[] NetworkedObjects { get; protected set; }
        public List<Projectile> Projectiles { get; protected set; }
        
        public void Update(float deltaTime)
        {
            CheckCollisions();
        }

        protected void CheckCollisions()
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                for (int j = 0; i < Projectiles.Count; j++)
                {
                    if (i==j)
                        continue;

                    Projectiles[i].Collider.IsColliding(Projectiles[j].Collider);
                }                
            }

            for (int i = 0; i < Projectiles.Count; i++)
            {
                for (int j = 0; j < NetworkPlayers.Length; j++)
                {
                    Projectiles[i].Collider.IsColliding(NetworkPlayers[i].PlayerCharacter.Collider);
                }
            }
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