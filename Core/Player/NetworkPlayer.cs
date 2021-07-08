using System;
using Core.Cards;
using LiteNetLib;
using LiteNetLib.Utils;

namespace Core.Player
{
    public interface INetworkPlayer
    {
        NetworkPlayerCharacter PlayerCharacter { get; }
        void Move(NetVector2 vector);
        void CastCardAcrossNetwork(int CardId);
        void Update();
    }
    
    public abstract class NetworkPlayer : INetworkPlayer
    {
        public NetworkPlayerCharacter PlayerCharacter { get; }
        public abstract void Move(NetVector2 vector);
        public abstract void CastCardAcrossNetwork(int CardId);
        public abstract void Update();
        public abstract void InitPlayerCharacterFromNetwork();

        protected ActionCardsQueueController _playerQueueController;
    }

    public abstract class NetworkPlayerCharacter
    {
        public NetworkPlayerStats PlayerInitialStats { get; }
        
        public NetworkPlayerStats PlayerCurrentStats { get; }
        
        public NetworkPlayerCharacter(NetworkPlayerStats playerInitialStats)
        {
            PlayerInitialStats = playerInitialStats;
            PlayerCurrentStats = PlayerInitialStats.Clone();
        }
    }

    public class NetworkPlayerStats : ICloneable<NetworkPlayerStats> 
    {
        public int Health;
        public int Energy;

        public NetworkPlayerStats Clone()
        {
            return new NetworkPlayerStats
            {
                Health = Health,
                Energy = Energy
            };
        }
    }

    public interface ICloneable<TObject> where TObject : class
    {
        TObject Clone();
    }
}