using System;
using Core.Entities;
using Core.Utils;

namespace Core.Collision
{
    public class BoxCollider
    {
        public event Action<BoxCollider> OnCollision;
        public INetworkedObject Owner { get; }
        public NetVector2 Center { get; set; }
        public NetVector2 Size { get; set; }
        
        public BoxCollider(INetworkedObject owner, NetVector2 size, NetVector2 center)
        {
            Owner = owner;
            Size = size;
            Center = center;
        }
        
        public bool IsColliding(BoxCollider other)
        {
            if (Center.X < other.Center.X + other.Size.X &&
                Center.X + Size.X > other.Center.X &&
                Center.Y < other.Center.Y + other.Size.Y && 
                Center.Y + Size.Y > other.Center.Y)
            {
                OnCollision?.Invoke(other);
                other.OnCollision?.Invoke(this);
                return true;
            }

            return false;
        }
    }
}