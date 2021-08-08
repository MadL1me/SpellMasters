using Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cards
{
    public class MoveCardData : CardData
    {
        public NetVector2 Direction { get; set; }
        public float Distance { get; set; }
    }
}
