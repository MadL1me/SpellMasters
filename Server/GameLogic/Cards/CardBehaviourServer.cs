using Core.Cards;
using Core.Entities;
using Core.GameLogic;
using Core.Utils;

namespace Server.GameLogic.Cards
{
    public class CardBehaviourServer : CardBehaviour
    {
        public override void ExecuteCast(BattleEnvironment battle, NetworkedPlayer player, ActionCard cardInst)
        {
            player.Energy -= cardInst.SharedData.EnergyCost;
        }

        static CardBehaviourServer()
        {
            var storage = FlyweightStorage<CardBehaviour>.Instance;

            storage.RegisterData(new MoveCardBehaviourServer());

            storage.RegisterData(new MoveCardBehaviourServer());

            storage.RegisterData(new MoveCardBehaviourServer());
        }
    }

}