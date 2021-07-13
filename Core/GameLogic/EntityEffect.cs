using System;
using System.Collections.Generic;
using Core.Player;

namespace Core.GameLogic
{
    public abstract class EntityEffect
    {
        public float TypeId { get; protected set; }
        private float _duration;

        public float Duration
        {
            get => _duration;
            set => _duration = value >= 0 ? value : 0;
        }

        protected float Power;

        public EntityEffect(float duration = 1, float power = 1)
        {
            Duration = duration;
            Power = power;
        }

        // I guess calls once is use
        public abstract void UseOnEntity(NetworkPlayerCharacter entity);

        public virtual void OnBeforeRemove(NetworkPlayerCharacter entity) {}

        // calls everytime then active
        public virtual void Update(NetworkPlayerCharacter entity, float deltaTime)
        {
            Duration -= deltaTime;
        }
    }
}