using Core.Cards;
using Core.Entities;
using Core.GameLogic;
using Core.Protocol.Packets;
using Core.Utils;
using MagicCardGame.Assets.Scripts.Protocol;

namespace MagicCardGame
{
    public class CardBehaviourClient : CardBehaviour
    {
        public override void ExecuteCast(BattleEnvironment battle, NetworkedPlayer player, ActionCard cardInst)
        {
            NetworkProvider.Connection.SendPacket(new C2SCastCard { CardId = cardInst.NumericId });
        }

        static CardBehaviourClient()
        {
            var storage = FlyweightStorage<CardBehaviour>.Instance;

            storage.RegisterData(new CardBehaviourClient());

            storage.RegisterData(new CardBehaviourClient());

            storage.RegisterData(new CardBehaviourClient());
        }
    }
}