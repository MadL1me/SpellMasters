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
        int PlayerId { get; }
        NetworkPlayerCharacter PlayerCharacter { get; }
        ActionCardsQueueController CardsQueueController { get; }
        void Move(NetVector2 vector);
        void CastCardAcrossNetwork(int cardId);
        void Update();
        void GetDamageAcrossNetwork(float damage);
    }

    public class NetworkPlayer : INetworkPlayer
    {
        public int PlayerId { get; set; }
        public NetworkPlayerCharacter PlayerCharacter { get; set; }
        public ActionCardsQueueController CardsQueueController { get; set; }

        public virtual void Move(NetVector2 vector)
        {
        }

        public virtual void CastCardAcrossNetwork(int cardId)
        {
        }

        public virtual void Update()
        {
        }

        public void GetDamageAcrossNetwork(float damage)
        {
            PlayerCharacter.PlayerCurrentStats.Health -= damage;
        }

        public NetworkPlayer()
        {
            PlayerCharacter = new NetworkPlayerCharacter(new NetworkPlayerStats());
            CardsQueueController = new ActionCardsQueueController(this, 5);
        }

        public virtual void InitPlayerCharacterFromNetwork()
        {
        }
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
            Collider = new BoxCollider(new NetVector2(5, 10)
                , new NetVector2(0, 0), this);
        }

        public void SetPosition(NetVector2 position)
        {
            Collider.Center = position;
        }
    }

    public class NetworkPlayerStats : ICloneable<NetworkPlayerStats>
    {
        public float Health { get; set; } = 100;
        public Stamina Stamina { get; set; } = new Stamina();
        public string DisplayName { get; set; } = "DefaultName";

        public NetworkPlayerStats Clone()
        {
            return new NetworkPlayerStats
            {
                Health = Health,
                Stamina = Stamina.Clone(),
                DisplayName = DisplayName
            };
        }
    }
}