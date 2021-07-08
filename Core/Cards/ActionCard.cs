using System.Collections.Generic;
using Core.Player;
using LiteNetLib.Utils;

namespace Core.Cards
{
    public abstract class ActionCard
    {
        public int CardId => _cardConfig.CardId;
        public int EnergyCost => _cardConfig.EnergyCost;

        public virtual void CastCard(INetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            networkPlayer.CastCardAcrossNetwork(CardId);
        }

        protected ActionCardConfig _cardConfig;

        public ActionCard(ActionCardConfig config)
        {
            _cardConfig = config;
        }
    }

    public class ActionCardsQueueController 
    {
        public INetworkPlayer Player { get; }
        
        public IDictionary<float, ActionCard> PlayedCardsAtTime { get; }
        
        public IEnumerable<ActionCard> CardsInHand { get; }
        
        public IEnumerable<ActionCard> NextDropCards { get; }
    }

    //Flyweight config for ActionCards
    public class ActionCardConfig
    {
        public readonly int CardId;
        public readonly int EnergyCost;
        public readonly string CardDescription;
        public readonly string CardName;

        public ActionCardConfig(int cardId, int energyCost = 5, string cardDescription = "", string cardName = "")
        {
            CardId = cardId;
            EnergyCost = energyCost;
            CardDescription = cardDescription;
            CardName = cardName;
        }
    }
}