using Core.Utils;

namespace Core.GameLogic
{
    /// <summary>
    /// Represents the main class which represents buff or effect on Entity. <para>&#160;</para>
    /// EntityEffect maps its Flyweight data: <see cref="EntityEffectData"/> to its behaviour <see cref="EntityEffectBehaviour"/>
    /// </summary>
    /// <seealso cref="FlyweightStorage{TData}"/>
    public class EntityEffect : FlyweightInstance<EntityEffectData>
    {
        public EntityEffect(uint id) : base(id) { }

        public EntityEffectBehaviour GetEntityEffectBehaviour() => 
            FlyweightStorage<EntityEffectBehaviour>.Instance.GetData(NumericId);
        
        public Type GetEntityEffectBehaviourAs<Type>() where Type : EntityEffectBehaviour => 
            (Type) FlyweightStorage<EntityEffectBehaviour>.Instance.GetData(NumericId);
    }
}