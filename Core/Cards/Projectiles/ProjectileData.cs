using Core.Utils;

namespace Core.Cards.Projectiles
{
    /// <summary>
    /// Represents a type of projectile
    /// </summary>
    public class ProjectileData
    {
        public float MaxLifetime { get; set; }
        public long DamageOnCollision { get; set; }
        public float KnockBackOnCollision { get; set; }
        public float MoveSpeed { get; set; }
        public NetVector2 Bounds { get; set; }

        static ProjectileData()
        {
            var storage = FlyweightStorage<ProjectileData>.Instance;
            
            
        }
    }
}