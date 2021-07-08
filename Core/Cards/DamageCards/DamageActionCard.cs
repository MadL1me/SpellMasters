using Core.Player;
using Core.Utils;

namespace Core.Cards.DamageCards
{
    public class DamageActionCard : ActionCard
    {
        public float Damage { get; }

        public DamageActionCard(ActionCardConfig config, float damage) : base(config)
        {
            Damage = damage;
        }
    }

    public class CloseAttack : DamageActionCard
    {
        public float AttackDistance { get; }

        public CloseAttack(ActionCardConfig config, float damage, float attackDistance) : base(config, damage)
        {
            AttackDistance = attackDistance;
        }

        public override void CastCard(INetworkPlayer networkPlayer, BattleEnvironment environment)
        {
            var closestCharacter = environment.GetClosestCharacterExcept(networkPlayer.PlayerCharacter.CharacterPosition, networkPlayer);
            if (NetVector2.Distance(networkPlayer.PlayerCharacter.CharacterPosition,
                networkPlayer.PlayerCharacter.CharacterPosition) <= AttackDistance)
            {
                closestCharacter.GetDamageAcrossNetwork(Damage);
            }
            
            base.CastCard(networkPlayer, environment);
        }
    }
}