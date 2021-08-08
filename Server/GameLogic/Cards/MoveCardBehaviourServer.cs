using Core.Cards;
using Core.Entities;
using Core.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.GameLogic.Cards
{
    public class MoveCardBehaviourServer : CardBehaviourServer
    {
        public override void ExecuteCast(BattleEnvironment battle, NetworkedPlayer player, ActionCard cardInst)
        {
            base.ExecuteCast(battle, player, cardInst);

            MoveCardData cardData = cardInst.SharedDataAs<MoveCardData>();

            player.Position += cardData.Direction * cardData.Distance;
        }
    }
}
