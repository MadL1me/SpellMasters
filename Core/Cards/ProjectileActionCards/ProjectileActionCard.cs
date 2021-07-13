using Core.Cards.Projectiles;

namespace Core.Cards.ProjectileActionCards
{
    public class ProjectileActionCard : ActionCard
    {
        public ProjectileConfig ProjectileConfig { get; }
        public int ProjectilesCount { get; }

        public ProjectileActionCard(ActionCardConfig config, ProjectileConfig projConfig, int count) 
            : base(config)
        {
            ProjectileConfig = projConfig;
            ProjectilesCount = count;
        }
    }
}