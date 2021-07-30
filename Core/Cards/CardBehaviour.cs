using Core.Entities;
using Core.GameLogic;

namespace Core.Cards
{
    /// <summary>
    /// Represents the behaviour of a card (what happens when it's casted)
    /// </summary>
    public abstract class CardBehaviour
    {
        public abstract void ExecuteCast(BattleEnvironment battle, NetworkedPlayer player, ActionCard cardInst);
    }
}