using Core.Cards.Types;
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
        public virtual void Cast(BattleEnvironment battle, NetworkedPlayer player) {}

        static CardData()
        {
            var storage = FlyweightStorage<CardData>.Instance;

            // 0: MoveCard
            storage.RegisterData(new MoveCardData());
        }
    }
}