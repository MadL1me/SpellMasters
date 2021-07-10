using System.Collections;
using System.Collections.Generic;
using System;
using Core.Cards;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MagicCardGame
{
    public class CardElement : MonoBehaviour, IPointerClickHandler
    {
        public float SlideSpeed = 200;

        public ActionCard CardType { get; protected set; }
        public CardHolder ParentHolder { get; set; }
        public Sprite Image { get; set; }
        public RectTransform Rect { get; set; }

        protected bool test = false;

        void Start()
        {
            Rect = gameObject.GetComponent<RectTransform>();
        }

        public static CardElement CreateFromActionCard(ActionCard cardData)
        {
            Sprite image = /*Resources.Load($"RawSprites/{cardData.CardName}.png");*/null;
            if (image == null)
                throw new NullReferenceException("Can't find related resourse");

            GameObject cardElementObject =  Instantiate(CardUIHandler.Hr.CardElementPrefab);
            CardElement component = cardElementObject.GetComponent<CardElement>();
            component.Image = image;
            component.CardType = cardData;

            return component;
        }

        //s-sempai clicked on me!!!!
        public void OnPointerClick(PointerEventData eventData)
        {
            ParentHolder.CardWasClicked(this);
        }

        public void Slide(Vector2 vector)
        {
            float duration = vector.magnitude / SlideSpeed;
            transform.DOMove((Vector2)transform.position + vector, duration);
        }

        void Update()
        {
            if(!test)
            {
                CardUIHandler.Hr.MainHolder.PutCard(this, 1);
                test = true;
            }
        }
    }
}
