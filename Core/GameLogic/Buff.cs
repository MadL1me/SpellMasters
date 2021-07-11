using System;
using System.Collections.Generic;

namespace Core.GameLogic
{
    public enum BuffType
    {
        Water,
        Fire,
        Electricity,
        Bleeding,
        StaminaRegeneration,
        HealthRegeneration,
        Speed,
    }

    public abstract class Buff
    {
        private float _duration;

        public float Duration
        {
            get => _duration;
            set => _duration = value >= 0 ? value : 0;
        }

        protected float Power;

        public Buff(float duration = 1, float power = 1)
        {
            Duration = duration;
            Power = power;
        }

        public abstract string GetSpriteName();

        public abstract void Use(Entity entity);

        public abstract void Update(Entity entity, float deltaTime);
    }
}