using Core.Cards;
using Core.Entities;
using Core.GameLogic;
using Core.Utils;

namespace Core.GameLogic
{
    /// <summary>
    /// Represents the configuration for effect on Entity
    /// </summary>
    public class EntityEffectData
    { 
        public float Power { get; set; }

        public float EffectName { get; set; }
        
        public string EffectDescription { get; set; }
        public float Duration { get; set; }

        public EntityEffectData(float duration = 1, float power = 1)
        {
            Duration = duration;
            Power = power;
        }
        
        static EntityEffectData()
        {
            var storage = FlyweightStorage<EntityEffectData>.Instance;
        }
    }
}