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
            storage.RegisterData(new MoveCardData
            {
                CardName = "moveRight",
                CardDescription = "Move pipa right",
                EnergyCost = 3,
                Direction = new NetVector2(1, 0),
                Distance = 5
            });

            storage.RegisterData(new MoveCardData
            {
                CardName = "moveLeft",
                CardDescription = "Move pipa left",
                EnergyCost = 3,
                Direction = new NetVector2(-1, 0),
                Distance = 5
            });

            storage.RegisterData(new MoveCardData
            {
                CardName = "moveUp",
                CardDescription = "Move pipa up",
                EnergyCost = 6,
                Direction = new NetVector2(0, 1),
                Distance = 5
            });
        }
    }
}