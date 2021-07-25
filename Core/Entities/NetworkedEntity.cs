using Core.Utils;

namespace Core.Entities
{
    /// <summary>
    /// Represents any networked entity
    /// </summary>
    public abstract class NetworkedEntity : FlyweightInstance<EntityData>, INetworkedObject
    {
        /// <summary>
        /// Id used for tracking the entity over the network
        /// </summary>
        public uint NetworkId { get; protected set; }
        
        public virtual NetVector2 Position { get; set; }

        public NetworkedEntity(uint typeId, uint networkId) : base(typeId)
        {
            NetworkId = networkId;
        }
    }
}