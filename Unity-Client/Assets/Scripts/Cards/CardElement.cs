using System.Collections;
using System.Collections.Generic;
using Core.Cards;
using DG.Tweening;
using UnityEngine;

namespace MagicCardGame
{
    public class CardElement : MonoBehaviour
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

        //s-sempai clicked on me!!!!
        public void wasClicked()
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
