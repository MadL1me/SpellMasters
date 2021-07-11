using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicCardGame
{
    public class CardUIHandler : MonoBehaviour
    {
        [SerializeField]
        public CardHolder MainHolder;

        [SerializeField]
        public GameObject CardElementPrefab;

        public static CardUIHandler Hr { get; set; }

        private void Awake()
        {
            if (Hr != null)
                DestroyImmediate(gameObject);
            else
                Hr = this;
        }
    }
}
