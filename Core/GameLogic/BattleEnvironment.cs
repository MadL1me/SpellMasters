using System;
using System.Collections.Generic;
using System.Linq;
using Core.Cards.Projectiles;
using Core.Collision;
using Core.Utils;

namespace Core.Player
{
    public class BattleEnvironment
    {
        public NetworkPlayer[] NetworkPlayers { get; set; }
       
        public List<Projectile> Projectiles { get; protected set; }
        public PhysicsEngine PhysicsEngine { get; protected set; } = new PhysicsEngine();

        public BattleEnvironment(int lobbySize)
        {
            NetworkPlayers = new NetworkPlayer[lobbySize];
        }
        
        public void Update(float deltaTime)
        {
            PhysicsEngine.Update(deltaTime);
        }

        public bool TryCastCardByPlayer(int playerId, int cardInHandIndex)
        {
            var player = NetworkPlayers.FirstOrDefault(netPlayer => netPlayer.PlayerId == playerId);

            if (player is null)
                throw new ArgumentException();

            return player.CardsQueueController.TryCastCardAtIndex(cardInHandIndex, this);
        }
        
        public NetworkPlayer GetClosestCharacter(NetVector2 position)
        {
            var minDistance = float.MaxValue;
            NetworkPlayer closestPlayer = null;
            
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
        
        public NetworkPlayer GetClosestCharacterExcept(NetVector2 position, NetworkPlayer player)
        {
            var minDistance = float.MaxValue;
            NetworkPlayer closestPlayer = null;
            
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
}