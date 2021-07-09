using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicCardGame
{
    public class CardHolder : MonoBehaviour
    {
        //public Deck BindedDeck;

        public int Capacity = 5;
        protected CardSlot[] Slots { get; set; }

        public RectTransform Rect { get; set; }

        void Awake()
        {
            Slots = new CardSlot[Capacity];
        }

        void Start()
        {
            Rect = GetComponent<RectTransform>();
            InitSlots();
        }

        private Vector2 CalculateCardSlotOffset(int index)
        {
            Vector2 cardSlotSize = new Vector2(Rect.sizeDelta.x / Capacity, Rect.sizeDelta.y);
            Vector2 cardPosition = new Vector2();

            cardPosition.x = cardSlotSize.x * index + cardSlotSize.x / 2;
            cardPosition.y = transform.position.y;

            Vector2 positonRelativeToCenter = (Vector2)transform.position - Rect.sizeDelta / 2 + cardPosition;
            return positonRelativeToCenter;
        }

        protected void InitSlots()
        {
            for(int i = 0; i < Capacity; i++)
            {
                GameObject slotGameObject = CardSlot.Create(this, CalculateCardSlotOffset(i));
                Slots[i] = slotGameObject.GetComponent<CardSlot>();
            }
        }

        public void CardWasClicked(CardElement clickedCard)
        {
            bool wasFound = false;
            int cardIndex = -1;

            for (int i = 0; i < Slots.Length; i++)
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


        }

        public void PutCard(CardElement card, int index)
        {
            if (index > Capacity)
                throw new ArgumentException("Index is bigger than capacity");

            Vector2 newPosition = Slots[index].transform.position;

            Slots[index].PutCard(card);

            card.Slide(newPosition - (Vector2)card.transform.position);
        }
    }
}
