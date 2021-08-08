using Core.Cards;
using Core.Entities;
using Core.GameLogic;
using Core.Protocol.Packets;
using MagicCardGame.Assets.Scripts.Protocol;

namespace MagicCardGame
{
    public abstract class CardBehaviourClient : CardBehaviour
    {
        public override void ExecuteCast(BattleEnvironment battle, NetworkedPlayer player, ActionCard cardInst)
        {
            NetworkProvider.Connection.SendPacket(new C2SCastCard { CardId = cardInst.NumericId });
        }
    }
}