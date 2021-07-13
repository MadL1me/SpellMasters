namespace Core.Cards.DamageCards
{
    public abstract class DamageActionCard : ActionCard
    {
        public float Damage { get; }

        public DamageActionCard(ActionCardConfig config, float damage) : base(config)
        {
            Damage = damage;
        }
    }
}