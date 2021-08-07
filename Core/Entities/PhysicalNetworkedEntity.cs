using Core.Collision;
using Core.Utils;

namespace Core.Entities
{
    /// <summary>
    /// Represents a colliding physical networked entity
    /// </summary>
    public abstract class PhysicalNetworkedEntity : NetworkedEntity
    {
        public BoxCollider Collider { get; }
        public GravityComponent GravityComp { get; }
        
        public virtual NetVector2 Position
        {
            get => Collider.Center;
            set => Collider.Center = value;
        }

        public PhysicalNetworkedEntity(uint typeId, uint networkId) : base(typeId, networkId)
        {
            Collider = new BoxCollider(this, SharedData.Bounds, new NetVector2(0, 0));
            GravityComp = new GravityComponent(this);
        }

        /// <summary>
        /// This call is detrimental for an entity to be a real object
        /// </summary>
        public void BindToPhysicalEngine(PhysicsEngine engine)
        {
            engine.AddObjectToWorld(Collider);
            engine.AddObjectToWorld(GravityComp);
        }
    }
}