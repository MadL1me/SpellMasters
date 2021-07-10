using Core.Utils;

namespace Core.Entities
{
    public interface IReplicatedEntity
    {
        int TypeId { get;  }
        
        NetVector2 Position { get; }
        string DisplayName { get; }
        long Health { get; }
    }
}