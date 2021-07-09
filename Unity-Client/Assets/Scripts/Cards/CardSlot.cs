using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicCardGame
{
    public class CardSlot : MonoBehaviour
    {
        public CardHolder ParentHolder { get; set; }
        public CardElement Card { get; set; }
        public RectTransform Rect { get; set; }

        public static GameObject Create(CardHolder holder, Vector2 position)
        {
            GameObject slotObject = new GameObject();
            slotObject.name = "Slot";
            CardSlot component = slotObject.AddComponent<CardSlot>();
            component.Rect = slotObject.AddComponent<RectTransform>();

            slotObject.transform.parent = holder.transform;
            slotObject.transform.position = position;

            component.ParentHolder = holder;
            component.Rect.sizeDelta = new Vector2(holder.Rect.sizeDelta.x / holder.Capacity,
                holder.Rect.sizeDelta.y);
                    
            return slotObject;
        }

        public void PutCard(CardElement card)
        {
            card.transform.parent = transform;
            Card = card;
        }
    }
}
