using System.Threading.Tasks;
using Core.Player;
using Core.Utils;

namespace Core.Cards.DamageCards
{
    public class CloseAttackCard : DamageActionCard
    {
        public float AttackDistance { get; }

        public CloseAttackCard(ActionCardConfig config, float damage, float attackDistance) : base(config, damage)
        {
            AttackDistance = attackDistance;
        }

        public async override Task CastCard(INetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            var closestCharacter = environment.GetClosestCharacterExcept(networkPlayer.PlayerCharacter.Position, networkPlayer);
            if (NetVector2.Distance(networkPlayer.PlayerCharacter.Position,
                networkPlayer.PlayerCharacter.Position) <= AttackDistance)
            {
                closestCharacter.GetDamageAcrossNetwork(Damage);
            }
            
            base.CastCard(networkPlayer, environment);
        }
    }
}