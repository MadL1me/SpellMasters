using Core.Cards.DamageCards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cards
{
    public static class CardsDatabase
    {
        public static  Dictionary<int, ActionCard> Cards;

        static CardsDatabase()
        {
            Cards = new Dictionary<int, ActionCard>();
            InitialFill();
        }

        private static void InitialFill()
        {
            AddCard(new MoveLeftCard(new ActionCardConfig(0, 5, "moveLeft1", "casualWalk"), new Utils.NetVector2(1, 0)));
            AddCard(new CloseAttackCard(new ActionCardConfig(1, 10, "throwPipa", "throwSomething"), 10, 5));

        }

        public static void AddCard(ActionCard card)
        {
            try
            {
                Cards.Add(card.CardId, card);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("this index is occupied");
            }
        }

        public static ActionCard GetCard(int id)
        {
            return Cards[id];
        }

    }
}
