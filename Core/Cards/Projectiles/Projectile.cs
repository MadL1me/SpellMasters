using System;
using Core.Collision;
using Core.Entities;
using Core.Player;
using Core.Utils;

namespace Core.Cards.Projectiles
{
    public class Projectile : INetworkObject
    {
        public event Action<Projectile> OnDestroy;
        public int TypeId => Config.TypeId;
        public float MaxLifetime => Config.MaxLifetime;
        public bool IsMoving => Config.MovingSpeed != 0 && Config.MoveVector != NetVector2.Zero;
        public bool IsDestroyed { get; protected set; }
        public NetVector2 Position
        {
            get => Collider.Center;
            set => Collider.Center = value;
        }
        public BoxCollider Collider { get; protected set; }
        public ProjectileConfig Config { get; protected set; }
        public float Lifetime { get; protected set; }

        public Projectile(NetVector2 position, NetVector2 size, ProjectileConfig config)
        {
            Collider = new BoxCollider(size, position, this);
            Collider.OnCollision += OnCollision;
            Config = config;
        }

        public void SetPosition(NetVector2 position)
        {
            Collider.Center = position;
        }
        
        public virtual void Update(float deltaTime)
        {
            if (IsDestroyed)
                return;
            
            Lifetime += deltaTime;
            
            if (Lifetime >= MaxLifetime)
                Destroy();
            
            Move(deltaTime);
        }

        protected virtual void Move(float deltaTime)
        {
            Position = Config.MoveVector * deltaTime * Config.MovingSpeed;
        }

        protected virtual void OnCollision(BoxCollider other)
        {
            if (other.Container is NetworkPlayerCharacter player)
                OnPlayerCollision(player);
        }
        
        protected virtual void OnPlayerCollision(NetworkPlayerCharacter playerCharacter)
        {
            playerCharacter.PlayerCurrentStats.Health -= Config.DamageOnCollision;
        }

        public virtual void Destroy()
        {
            OnDestroy?.Invoke(this);
            IsDestroyed = true;
        }
    }

    public class ProjectileConfig
    {
        public readonly int TypeId;
        public readonly float MaxLifetime = 5;
        public readonly float DamageOnCollision = 0;
        public readonly float KnockBackOnCollision = 0;
        public readonly float MovingSpeed = 0;
        public readonly NetVector2 MoveVector;

        public ProjectileConfig(int typeId, float maxLifetime, float damageOnCollision, 
            float knockBackOnCollision, float movingSpeed, NetVector2 moveVector)
        {
            TypeId = typeId;
            MaxLifetime = maxLifetime;
            DamageOnCollision = damageOnCollision;
            KnockBackOnCollision = knockBackOnCollision;
            MovingSpeed = movingSpeed;
            MoveVector = moveVector;
        }
    }
    
    public class FireballProjectile : Projectile
    {
        public FireballProjectile(NetVector2 position, NetVector2 size, ProjectileConfig config) 
            : base(position, size, config) { }

        protected override void OnPlayerCollision(NetworkPlayerCharacter playerCharacter)
        {
             //add fire debuff
        }
    }

    public class StoneWallProjectile : Projectile
    {
        public StoneWallProjectile(NetVector2 position, NetVector2 size, ProjectileConfig config) 
            : base(position, size, config) { }
    }
}