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

        /// <summary>
        /// Called when the player is about to cast the card
        /// </summary>
        public void ExecuteCast(BattleEnvironment battle, NetworkedPlayer player, ActionCard cardInst)
        {
            FlyweightStorage<CardBehaviour>.Instance.GetData(cardInst.NumericId).ExecuteCast(battle, player, cardInst);
        }

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