using System.Threading.Tasks;
using Core.GameLogic.Buffs;
using Core.Player;

namespace Core.Cards.ActionCards
{
    public class ElectroAttack : ActionCard
    {
        public ElectroAttack(ActionCardConfig config) : base(config) { }

        public async override Task CastCard(NetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            base.CastCard(networkPlayer, environment);
            new ElectroEntityEffect().UseOnEntity(networkPlayer.PlayerCharacter);
        }
    }
}