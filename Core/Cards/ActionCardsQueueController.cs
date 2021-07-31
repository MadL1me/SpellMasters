using System;
using System.Collections.Generic;
using Core.Entities;
using Core.GameLogic;

namespace Core.Cards
{
    /// <summary>
    /// Represents players "hand" synchronized across network.
    /// Purpose of this class is to contain network player cards he can cast at current time,
    /// and check if player can cast card or not.
    /// </summary>
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
            // return NextDropCards.Dequeue();
            var card = new ActionCard(0);

            return card;
        }
        
        /// <summary>
        /// Try to cast card at position in hand. For example, if 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="environment">Battle environment suitable for this player lobby</param>
        /// <returns>Success of casting card - true or false</returns>
        /// <exception cref="ArgumentOutOfRangeException">index cannot be less than 0 or more than hand length</exception>
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