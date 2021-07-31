using Core.Entities;
using Core.GameLogic;
using Core.Utils;

namespace Core.Cards
{
    /// <summary>
    /// Represents a card type
    /// </summary>
    public class CardData
    {
        public int EnergyCost { get; set; }
        public string CardDescription { get; set; }
        public string CardName { get; set; }

        static CardData()
        {
            var storage = FlyweightStorage<CardData>.Instance;

            // 0: MoveCard
            storage.RegisterData(new CardData
            {
                CardName = "moveRight",
                CardDescription = "Move pipa",
                EnergyCost = 3
            });
        }
    }
}