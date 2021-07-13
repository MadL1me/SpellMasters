using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Player;

namespace Core.Cards
{
    public class ActionCard
    {
        public string CardName => СardConfig.CardName;
        public int CardId => СardConfig.CardId;
        public int EnergyCost => СardConfig.EnergyCost;

        protected ActionCardConfig СardConfig;

        public ActionCard(ActionCardConfig config)
        {
            СardConfig = config;
        }
        
        public virtual async Task CastCard(INetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            await Task.Delay(СardConfig.CastDelay);
            networkPlayer.CastCardAcrossNetwork(CardId);
        }
    }

    public class ActionCardsQueueController 
    {
        public INetworkPlayer Player { get; }
        public ActionCard[] CardsInHand { get; } 

        public Queue<ActionCard> NextDropCards { get; } = new Queue<ActionCard>();
        
        public Dictionary<float, ActionCard> PlayedCardsAtTime { get; } = new Dictionary<float, ActionCard>();
        
        public ActionCardsQueueController(INetworkPlayer player, int cardsCount)
        {
            Player = player;
            CardsInHand = new ActionCard[cardsCount];
        }

        public ActionCard GetCardAtPosition(int position)
        {
            return null;
        }
    }
    
    public class ActionCardConfig
    {
        public readonly int CardId;
        public readonly int EnergyCost;
        public readonly int CastDelay;
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