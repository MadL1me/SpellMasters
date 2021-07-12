using Core.Collision;
using Core.Entities;
using Core.Utils;

namespace Core.Cards.Projectiles
{
    public abstract class Projectile : INetworkObject
    {
        public int TypeId { get; protected set; }
        public BoxCollider Collider { get; protected set; }
        
        public NetVector2 Position => Collider.Center;
        
        public Projectile(NetVector2 position, NetVector2 size, int typeId)
        {
            new BoxCollider(size, position, this);
            TypeId = typeId;
        }
    }

    public class FireballProjectile : Projectile
    {
        public FireballProjectile(NetVector2 position, NetVector2 size, int typeId) : base(position, size, typeId)
        {
            
        }
    }

    public class StoneWallProjectile : Projectile
    {
        public StoneWallProjectile(NetVector2 position, NetVector2 size, int typeId) : base(position, size, typeId)
        {
            
        }
    }
}