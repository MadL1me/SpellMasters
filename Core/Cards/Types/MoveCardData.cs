using Core.Entities;
using Core.GameLogic;

namespace Core.Cards.Types
{
    /// <summary>
    /// Movement card, mainly for testing
    /// </summary>
    public class MoveCardData : CardData
    {
        public MoveCardData()
        {
            EnergyCost = 3;
            CardDescription = "Move pipa";
            CardName = "moveRight";
        }

        public override void Cast(BattleEnvironment battle, NetworkedPlayer player)
        {
            
        }
    }
}