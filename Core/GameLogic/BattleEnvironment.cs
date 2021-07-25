using System;
using System.Collections.Generic;
using System.Linq;
using Core.Cards.Projectiles;
using Core.Collision;
using Core.Entities;
using Core.Protocol;
using Core.Utils;

namespace Core.GameLogic
{
    public class BattleEnvironment
    {
        public NetworkedPlayer[] NetworkPlayers { get; set; }
       
        public List<Projectile> Projectiles { get; protected set; }
        public PhysicsEngine PhysicsEngine { get; protected set; } = new PhysicsEngine();

        public BattleEnvironment(int lobbySize)
        {
            NetworkPlayers = new NetworkedPlayer[lobbySize];
        }
        
        public void Update(float deltaTime)
        {
            PhysicsEngine.Update(deltaTime);
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