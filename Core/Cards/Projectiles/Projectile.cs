using System;
using Core.Collision;
using Core.Entities;
using Core.GameLogic;
using Core.Utils;

namespace Core.Cards.Projectiles
{
    public class Projectile : FlyweightInstance<ProjectileData>, INetworkedObject
    {
        public virtual NetVector2 Position
        {
            get => Collider.Center;
            set => Collider.Center = value;
        }
        
        public bool IsDestroyed { get; protected set; }
        public BoxCollider Collider { get; protected set; }
        public float Lifetime { get; protected set; }
        public NetVector2 Direction { get; protected set; }

        public Projectile(uint typeId, NetVector2 position, NetVector2 direction)
            : base(typeId)
        {
            Collider = new BoxCollider(this, SharedData.Bounds, position);
            Collider.OnCollision += OnCollision;

            Direction = direction;
        }
        
        public virtual void Update(float deltaTime)
        {
            if (IsDestroyed)
                return;
            
            Lifetime += deltaTime;
            
            if (Lifetime >= SharedData.MaxLifetime)
                Destroy();
            
            Move(deltaTime);
        }

        protected virtual void Move(float deltaTime)
        {
            Position = Direction * deltaTime * SharedData.MoveSpeed;
        }

        protected virtual void OnCollision(BoxCollider other)
        {
            if (other.Owner is NetworkedPlayer player)
                OnPlayerCollision(player);
        }
        
        protected virtual void OnPlayerCollision(NetworkedPlayer playerCharacter)
        {
            playerCharacter.Health -= SharedData.DamageOnCollision;
        }

        public virtual void Destroy()
        {
            IsDestroyed = true;
        }
    }
}