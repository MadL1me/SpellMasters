using Core.Utils;

namespace Core.Entities
{
    public interface IReplicatedEntity
    {
        public int TypeId { get;  }
        public EntityType Type => EntityType.GetTypeById(TypeId);
        
        public NetVector2 Position { get; }
        public string DisplayName { get; }
        public long Health { get; }
    }
}