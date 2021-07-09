using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicCardGame
{
    public class CardUIHandler : MonoBehaviour
    {
        public CardHolder MainHolder;

        public static CardUIHandler Hr { get; set; }

        void Awake()
        {
            if (Hr != null)
                DestroyImmediate(gameObject);
            else
                Hr = this;
        }
    }
}
