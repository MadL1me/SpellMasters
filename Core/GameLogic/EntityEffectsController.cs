using System.Collections.Generic;
using Core.Cards;
using Core.Entities;
using Core.Utils;

namespace Core.GameLogic
{
    /// <summary>
    /// Represents effects controller on corresponding <see cref="NetworkedPlayer"/>. <para>&#160;</para>
    /// Basically, its a list with effects, which contains gameloop with logic for effects execution.
    /// </summary>
    /// <seealso cref="EntityEffect"/>
    public class EntityEffectsController
    {
        public List<EntityEffect> Effects { get; private set; } = new List<EntityEffect>();
        public int MaxEffects { get; private set; }
        public NetworkedPlayer NetworkedPlayer { get; private set; }
        
        public EntityEffectsController(NetworkedPlayer player, int maxEffects = 5)
        {
            NetworkedPlayer = player;
            MaxEffects = maxEffects;
        }

        public bool TryAddEffect(EntityEffect effect)
        {
            var behaviour = effect.GetEntityEffectBehaviour();
            
            foreach (var entityEffect in Effects)
                behaviour.InteractWithOtherEffect(entityEffect);

            if (Effects.Count >= MaxEffects)
                return false;
            
            Effects.Add(effect);
            behaviour.OnEffectEnd += (eff) => TryRemoveEffect(eff);
            
            return true;
        }

        public bool TryRemoveEffect(EntityEffect effect)
        {
            effect.GetEntityEffectBehaviour().OnBeforeRemove(NetworkedPlayer);
            return Effects.Remove(effect);
        }

        public void UpdateEffects(float deltaTime)
        {
            foreach (var entityEffect in Effects)
                FlyweightStorage<EntityEffectBehaviour>
                    .Instance
                    .GetData(entityEffect.NumericId)
                    .Update(entityEffect, NetworkedPlayer, deltaTime);
        }
    }
}