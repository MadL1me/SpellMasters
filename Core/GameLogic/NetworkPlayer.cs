using System.Collections.Generic;
using Core.Cards;
using Core.Collision;
using Core.Entities;
using Core.GameLogic;
using Core.Utils;

namespace Core.Player
{
    public interface INetworkPlayer
    {
        NetworkPlayerCharacter PlayerCharacter { get; }
        void Move(NetVector2 vector);
        void CastCardAcrossNetwork(int CardId);
        void Update();
        void GetDamageAcrossNetwork(float damage);
    }
    
    public abstract class NetworkPlayer : INetworkPlayer
    {
        public NetworkPlayerCharacter PlayerCharacter { get; }
        public abstract void Move(NetVector2 vector);
        public abstract void CastCardAcrossNetwork(int CardId);
        public abstract void Update();
        public void GetDamageAcrossNetwork(float damage)
        {
            PlayerCharacter.PlayerCurrentStats.Health -= damage;
        }

        public abstract void InitPlayerCharacterFromNetwork();

        protected ActionCardsQueueController _playerQueueController;
    }

    public class NetworkPlayerCharacter : INetworkObject
    {
        public int TypeId { get; }
        public NetworkPlayerStats PlayerInitialStats { get; }
        public NetworkPlayerStats PlayerCurrentStats { get; }
        public NetVector2 Position => Collider.Center;
        public BoxCollider Collider { get; }
        public List<EntityEffect> PlayerEffects { get; }
        public bool CanMove { get; set; }
        
        public NetworkPlayerCharacter(NetworkPlayerStats playerInitialStats)
        {
            PlayerInitialStats = playerInitialStats;
            PlayerCurrentStats = PlayerInitialStats.Clone();
            Collider = new BoxCollider(new NetVector2(5,10)
                ,new NetVector2(0,0), this);
        }
        
        public void SetPosition(NetVector2 position)
        {
            Collider.Center = position;
        }
    }

    public class NetworkPlayerStats : ICloneable<NetworkPlayerStats> 
    {
        public float Health { get; set; }
        public Stamina Energy { get; set; }
        public string DisplayName { get; set; }
        
        public NetworkPlayerStats Clone()
        {
            return new NetworkPlayerStats
            {
                Health = Health,
                Energy = Energy.Clone(),
                DisplayName = DisplayName
            };
        }
    }
}