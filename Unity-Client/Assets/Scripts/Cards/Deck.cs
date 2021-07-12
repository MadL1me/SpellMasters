using System;
using Core.Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicCardGame
{
    public class Deck : MonoBehaviour
    {
        public CardElement AskForCard()
        {
            var askedCardType = QueryCardFromServerDeck();
            var card = CardElement.CreateFromActionCard(askedCardType);
            card.transform.position = transform.position;

            return card;
        }

        protected ActionCard QueryCardFromServerDeck()
        {
            //Not implemented due lack of networking support at this moment
            var testConfig = new ActionCardConfig(1, 10, "testCard", "WaterAttack");
            var card = new ActionCard(testConfig);

            return card;
        }
    }
}
