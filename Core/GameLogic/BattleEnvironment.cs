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
        public INetworkPlayer[] NetworkPlayers { get; protected set; }
       
        public List<Projectile> Projectiles { get; protected set; }
        public PhysicsEngine PhysicsEngine { get; protected set; } = new PhysicsEngine();
        
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
        public INetworkPlayer FirstPlayer => NetworkPlayers[0];
        public INetworkPlayer SecondPlayer => NetworkPlayers[1];
        
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