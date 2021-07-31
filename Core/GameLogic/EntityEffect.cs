using Core.Utils;

namespace Core.GameLogic
{
    public class EntityEffect : FlyweightInstance<EntityEffectData>
    {
        public EntityEffect(uint id) : base(id) { }

        public EntityEffectBehaviour GetEntityEffectBehaviour() => 
            FlyweightStorage<EntityEffectBehaviour>.Instance.GetData(NumericId);
        
        public Type GetEntityEffectBehaviourAs<Type>() where Type : EntityEffectBehaviour => 
            (Type) FlyweightStorage<EntityEffectBehaviour>.Instance.GetData(NumericId);
    }
}