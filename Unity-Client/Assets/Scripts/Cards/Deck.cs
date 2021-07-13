using System;
using Core.Cards;
using System.Collections;
using System.Collections.Generic;
using MagicCardGame.Assets.Scripts.GameLogic;
using MagicCardGame.Network;
using UnityEngine;
using NetworkPlayer = Core.Player.NetworkPlayer;

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
