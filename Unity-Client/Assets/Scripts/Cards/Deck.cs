using Core.Cards;
using MagicCardGame.Assets.Scripts.GameLogic;
using MagicCardGame.Network;
using UnityEngine;

namespace MagicCardGame
{
    public class Deck : MonoBehaviour
    {        
        public CardElement AskForCard()
        {
            var askedCardType = QueryCardFromDeck();
            var card = CardElement.CreateFromActionCard(askedCardType);
            card.transform.position = transform.position;

            return card;
        }

        protected ActionCard QueryCardFromDeck()
        {
            var networkPlayer = BattleEnvironmentClient.Current.LocalPlayer;

            //Not implemented due lack of networking support at this moment
            return networkPlayer.CardsQueueController.TryGetNextCard();
        }
    }
}
