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
            ActionCard askedCardType = QueryCardFromServerDeck();
            CardElement card = CardElement.CreateFromActionCard(askedCardType);
            card.transform.position = transform.position;

            return card;
        }

        protected ActionCard QueryCardFromServerDeck()
        {
            //Not implented due lack of networking support at this moment
            ActionCardConfig testConfig = new ActionCardConfig(1, 10, "testCard", "WaterAttack");

            throw new NotImplementedException();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
