using System;
using Core.Entities;

namespace Core.GameLogic
{
    /// <summary>
    /// Represents the logic for continuous buff or effect on Entity (For example burning for 5 second, etc)
    /// </summary>
    /// <seealso cref="EntityEffect"/>
    public abstract class EntityEffectBehaviour
    {
        public event Action<EntityEffect> OnEffectEnd;
        
        public readonly EntityEffect Effect;

        public float Duration
        {
            get => Effect.SharedData.Duration;
            set => Effect.SharedData.Duration = value >= 0 ? value : 0;
        }
        
        public EntityEffectBehaviour(EntityEffect effect)
        {
            Effect = effect;
        }
        
        public abstract void UseOnEntity(NetworkedPlayer entity);

        public virtual void InteractWithOtherEffect(EntityEffect effect) { }
        
        public virtual void OnBeforeRemove(NetworkedPlayer entity) { }
        
        public virtual void Update(EntityEffect effect, NetworkedPlayer entity, float deltaTime)
        {
            Duration -= deltaTime;
            if (Duration <= 0)
                OnEffectEnd?.Invoke(Effect);
        }
    }
}