using System.Threading.Tasks;
using Core.Player;

namespace Core.Cards.ActionCards
{
    public class TeleportCard : ActionCard
    {
        public TeleportCard(ActionCardConfig config) : base(config) { }

        public async override Task CastCard(NetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            base.CastCard(networkPlayer, environment);
            networkPlayer.PlayerCharacter
                .SetPosition(environment.GetClosestCharacterExcept(
                    networkPlayer.PlayerCharacter.Position, networkPlayer).PlayerCharacter.Position);
        }
    }
}