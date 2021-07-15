using Core.Cards;
using MagicCardGame.Network;
using UnityEngine;

namespace MagicCardGame
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private NetworkPlayerClientView _networkPlayer;
        
        public CardElement AskForCard()
        {
            var askedCardType = QueryCardFromDeck();
            var card = CardElement.CreateFromActionCard(askedCardType);
            card.transform.position = transform.position;

            return card;
        }

        protected ActionCard QueryCardFromDeck()
        {
            _networkPlayer.NetworkPlayer.CardsQueueController.TryGetNextCard();
            //Not implemented due lack of networking support at this moment
            return _networkPlayer.NetworkPlayer.CardsQueueController.TryGetNextCard();;
        }
    }
}
