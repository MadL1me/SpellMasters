namespace Core.Entities
{
    public interface IReplicatedObject : INetworkObject
    {
        string DisplayName { get; }
        long Health { get; }
    }
}