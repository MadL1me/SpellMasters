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
            var slotObject = new GameObject {name = "Slot"};
            var component = slotObject.AddComponent<CardSlot>();
            component.Rect = slotObject.AddComponent<RectTransform>();

            slotObject.transform.SetParent(holder.transform);
            slotObject.transform.position = position;

            component.ParentHolder = holder;
            component.Rect.sizeDelta = new Vector2(holder.Rect.sizeDelta.x / holder.Capacity, 0);

            return slotObject;
        }

        public void PutCard(CardElement card)
        {
            card.transform.SetParent(transform);
            Card = card;
        }

        public void RemoveCard()
        {
            Destroy(Card.gameObject);
            Card = null;
        }
    }
}