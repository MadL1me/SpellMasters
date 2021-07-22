using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Player;

namespace Core.Cards
{
    public class ActionCard
    {
        public string CardName => СardConfig.CardName;
        public int CardId => СardConfig.CardId;
        public int StaminaCost => СardConfig.EnergyCost;

        protected ActionCardConfig СardConfig;

        public ActionCard(ActionCardConfig config)
        {
            СardConfig = config;
        }
        
        public virtual async Task CastCard(NetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            await Task.Delay(СardConfig.CastDelay);
            networkPlayer.CastCardAcrossNetwork(CardId);
        }
    }

    public class ActionCardsQueueController 
    {
        public NetworkPlayer Player { get; }
        public ActionCard[] CardsInHand { get; } 
        public Queue<ActionCard> NextDropCards { get; } = new Queue<ActionCard>();
        public ActionCard this[int index] => CardsInHand[index];
        
        public ActionCardsQueueController(NetworkPlayer player, int cardsCount)
        {
            Player = player;
            CardsInHand = new ActionCard[cardsCount];
        }

        public ActionCard TryGetNextCard()
        {
            // now it doesnt work, but later will
            //return NextDropCards.Dequeue();
            var testConfig = new ActionCardConfig(1, 10, "testCard", "WaterAttack");
            var card = new ActionCard(testConfig);

            return card;
        }
        
        public bool TryCastCardAtIndex(int index, BattleEnvironment castingContext)
        {
            if (index <= 0 || index >= CardsInHand.Length)
                throw new ArgumentOutOfRangeException();

            var card = CardsInHand[index];
            
            if (Player.PlayerCharacter.PlayerCurrentStats.Stamina >= card.StaminaCost)
            {
                card.CastCard(Player, castingContext);
                Player.PlayerCharacter.PlayerCurrentStats.Stamina.Spend(card.StaminaCost);
                CardsInHand[index] = NextDropCards.Dequeue();
                return true;
            }

            return false;
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