using Core.Entities;
using Core.Utils;

namespace Core.Collision
{
    /// <summary>
    /// Simple gravity class, equivalent of Rigidbody gravity.
    /// </summary>
    public class GravityComponent
    {
        public INetworkedObject NetworkObject { get; }
        public NetVector2 Velocity { get; }
        
        public GravityComponent(INetworkedObject networkObject)
        {
            NetworkObject = networkObject;
        }
        
        public void Update(float deltaTime)
        {
            NetworkObject.Position += (NetworkObject.Position + Velocity) * deltaTime;
        }
    }
}