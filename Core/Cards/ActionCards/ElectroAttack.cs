using System.Threading.Tasks;
using Core.GameLogic.Buffs;
using Core.Player;

namespace Core.Cards.ActionCards
{
    public class ElectroAttack : ActionCard
    {
        public ElectroAttack(ActionCardConfig config) : base(config) { }

        public async override Task CastCard(INetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            base.CastCard(networkPlayer, environment);
            networkPlayer.PlayerCharacter.PlayerEffects.Add(new ElectroEntityEffect());
        }
    }
}