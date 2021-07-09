using Core.Utils;

namespace Core.Entities
{
    public abstract class NetworkedEntity : IReplicatedEntity
    {
        public int TypeId { get; protected set; }
        public EntityType Type => EntityType.GetTypeById(TypeId);
        
        public NetVector2 Position { get; protected set; }
        public string DisplayName { get; protected set; }
        public long Health { get; protected set; }

        protected NetworkedEntity(int id)
        {
            TypeId = id;
        }
    }
}