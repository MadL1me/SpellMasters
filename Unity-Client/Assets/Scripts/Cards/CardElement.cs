using System;
using Core.Cards;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MagicCardGame
{
    public class CardElement : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        protected float SlideSpeed = 200;
        public ActionCard CardType { get; protected set; }
        public CardHolder ParentHolder { get; set; }
        public Sprite Image { get; set; }
        public RectTransform Rect { get; set; }

        protected bool Test = false;

        private void Start()
        {
            Rect = gameObject.GetComponent<RectTransform>();
        }

        public static CardElement CreateFromActionCard(ActionCard cardData)
        {
            var image = Resources.Load<Sprite>($"RawSprites/{cardData.SharedData.CardName}");
            if (image == null)
                throw new NullReferenceException("Can't find related resource");

            var cardElementObject = Instantiate(CardUIHandler.Hr.CardElementPrefab);
            var component = cardElementObject.GetComponent<CardElement>();

            component.Image = image;
            var imageComponent = cardElementObject.GetComponentInChildren<Image>();
            imageComponent.sprite = image;
            component.CardType = cardData;

            return component;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ParentHolder.CardWasClicked(this);
        }

        public void Slide(Vector2 vector, float speed)
        {
            var duration = vector.magnitude / SlideSpeed * speed;
            transform.DOMove((Vector2)transform.position + vector, duration);
        }

        private void Update()
        {
            /*if(!Test)
            {
                CardUIHandler.Hr.MainHolder.PutCard(this, 1);
                Test = true;
            }*/
        }
    }
}
