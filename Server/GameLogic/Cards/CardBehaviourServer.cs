using Core.Cards;
using Core.Entities;
using Core.GameLogic;

namespace Server.GameLogic.Cards
{
    public abstract class CardBehaviourServer : CardBehaviour
    {
        public override void ExecuteCast(BattleEnvironment battle, NetworkedPlayer player, ActionCard cardInst)
        {
            player.Energy -= cardInst.SharedData.EnergyCost;
        }
    }

}