using Core.Entities;
using Core.Utils;

namespace Core.Collision
{
    public class GravityComponent
    {
        public INetworkObject NetworkObject { get; }
        public NetVector2 Velocity { get; }
        
        public GravityComponent(INetworkObject networkObject)
        {
            NetworkObject = networkObject;
        }
        
        public void Update(float deltaTime)
        {
            NetworkObject.SetPosition((NetworkObject.Position + Velocity) * deltaTime);
        }
    }
}