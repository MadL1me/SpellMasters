using Core.GameLogic;

namespace Core.GameLogic.Buffs
{
    /*public class FireEntityEffect : EntityEffect
    {
        public override void UseOnEntity(NetworkPlayerCharacter entity)
        {
            var appendBuff = false;
            foreach (var entityEffect in entity.PlayerEffects)
            {
                switch (entityEffect)
                {
                    case FireEntityEffect fireBuff:
                        entityEffect.Duration += Duration;
                        break;
                    case WetEntityEffect wetBuff:
                        // some vaporise effect
                        entityEffect.Duration = 0;
                        Duration = 0;
                        break;
                    default:
                        appendBuff = true;
                        break;
                }
            }

            if (appendBuff)
                entity.PlayerEffects.Add(this);
        }

        public override void Update(NetworkPlayerCharacter entity, float deltaTime)
        {
            entity.PlayerCurrentStats.Health -= deltaTime * Power;
        }
    }*/
}