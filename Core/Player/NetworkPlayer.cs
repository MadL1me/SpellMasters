using Core.Cards;
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

    public abstract class NetworkPlayerCharacter
    {
        public NetworkPlayerStats PlayerInitialStats { get; }
        public NetworkPlayerStats PlayerCurrentStats { get; }
        public NetVector2 CharacterPosition { get; }
        
        public NetworkPlayerCharacter(NetworkPlayerStats playerInitialStats)
        {
            PlayerInitialStats = playerInitialStats;
            PlayerCurrentStats = PlayerInitialStats.Clone();
        }
    }

    public class NetworkPlayerStats : ICloneable<NetworkPlayerStats> 
    {
        public float Health { get; set; }
        public int Energy { get; set; }

        public NetworkPlayerStats Clone()
        {
            return new NetworkPlayerStats
            {
                Health = Health,
                Energy = Energy
            };
        }
    }
}