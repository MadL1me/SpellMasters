using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.GameLogic;
using Core.Utils;

namespace Core.Cards
{
    public class ActionCard : FlyweightInstance<CardData>
    {
        public ActionCard(uint cardId) : base(cardId)
        { }
    }

    public class ActionCardsQueueController 
    {
        public NetworkedPlayer Player { get; }
        public ActionCard[] CardsInHand { get; } 
        public Queue<ActionCard> NextDropCards { get; } = new Queue<ActionCard>();
        
        public ActionCardsQueueController(NetworkedPlayer player, int cardsCount)
        {
            Player = player;
            CardsInHand = new ActionCard[cardsCount];
        }

        public ActionCard TryGetNextCard()
        {
            // now it doesnt work, but later will
            //return NextDropCards.Dequeue();
            var card = new ActionCard(0);

            return card;
        }
        
        public bool TryCastCardAtIndex(int index, BattleEnvironment environment)
        {
            if (index <= 0 || index >= CardsInHand.Length)
                throw new ArgumentOutOfRangeException();

            var card = CardsInHand[index];
            
            if (Player.Energy >= card.SharedData.EnergyCost)
            {
                // TODO Cast card here somehow
                //card.CastCard(Player, environment);
                Player.Energy -= card.SharedData.EnergyCost;
                CardsInHand[index] = NextDropCards.Dequeue();
                return true;
            }

            return false;
        }
    }
}