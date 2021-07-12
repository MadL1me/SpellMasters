using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicCardGame
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] protected Deck BindedDeck;

        [SerializeField] public int Capacity = 5;
        protected CardSlot[] Slots { get; set; }

        public RectTransform Rect { get; set; }

        private void Awake()
        {
            Slots = new CardSlot[Capacity];
        }

        private void Start()
        {
            Rect = GetComponent<RectTransform>();
            InitSlots();
            FirstFilling();
        }

        private void FirstFilling()
        {
            for (var i = 0; i < Slots.Length; i++)
                PutCard(BindedDeck.AskForCard(), i);
        }

        private Vector2 CalculateCardSlotOffset(int index)
        {
            var cardSlotSize = new Vector2(Rect.sizeDelta.x / Capacity, Rect.sizeDelta.y);
            var cardPosition = new Vector2();

            cardPosition.x = cardSlotSize.x * index + cardSlotSize.x / 2;
            cardPosition.y = transform.position.y;

            var positionRelativeToCenter = (Vector2) transform.position - Rect.sizeDelta / 2 + cardPosition;
            return positionRelativeToCenter;
        }

        protected void InitSlots()
        {
            for (var i = 0; i < Capacity; i++)
            {
                var slotGameObject = CardSlot.Create(this, CalculateCardSlotOffset(i));
                Slots[i] = slotGameObject.GetComponent<CardSlot>();
            }
        }

        public void CardWasClicked(CardElement clickedCard)
        {
            var wasFound = false;
            var cardIndex = -1;

            for (var i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].Card == clickedCard)
                {
                    wasFound = true;
                    cardIndex = i;
                }
            }

            //sanity check
            if (!wasFound)
                throw new KeyNotFoundException("Clicked card is not presented in Holder");

            //Slots[cardIndex].Card.CardType.CastCard();
            RemoveCardByIndex(cardIndex);
            var cardForReplacement = BindedDeck.AskForCard();
            PutCard(cardForReplacement, cardIndex);
        }

        public void PutCard(CardElement card, int index)
        {
            if (index > Capacity)
                throw new ArgumentException("Index is bigger than capacity");

            Vector2 newPosition = Slots[index].transform.position;

            Slots[index].PutCard(card);
            Slots[index].Card.ParentHolder = this;

            card.Slide(newPosition - (Vector2) card.transform.position, Mathf.Sqrt(Capacity - index));
        }

        public void RemoveCardByIndex(int index)
        {
            Slots[index].RemoveCard();
        }
    }
}