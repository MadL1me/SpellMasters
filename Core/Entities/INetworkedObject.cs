using Core.Utils;

namespace Core.Entities
{
    /// <summary>
    /// Interface for all objects we want to synchronise across network
    /// </summary>
    public interface INetworkedObject
    {
        NetVector2 Position { get; set; }
    }
}