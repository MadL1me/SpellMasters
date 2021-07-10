using System.Collections.Generic;
using Core.Player;

namespace Core.Cards
{
    public class ActionCard
    {
        public string CardName => СardConfig.CardName;
        public int CardId => СardConfig.CardId;
        public int EnergyCost => СardConfig.EnergyCost;

        public virtual void CastCard(INetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            networkPlayer.CastCardAcrossNetwork(CardId);
        }

        protected ActionCardConfig СardConfig;

        public ActionCard(ActionCardConfig config)
        {
            СardConfig = config;
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