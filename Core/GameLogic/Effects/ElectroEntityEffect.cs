using Core.GameLogic;

namespace Core.GameLogic.Buffs
{
    /*public class ElectroEntityEffect : EntityEffect
    {
        public override void UseOnEntity(NetworkPlayerCharacter entity)
        {
            var appendBuff = false;
            foreach (var entityEffect in entity.PlayerEffects)
            {
                switch (entityEffect)
                {
                    case WetEntityEffect wetBuff:
                        entity.PlayerCurrentStats.Health -= Power;
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
            entity.CanMove = false;
        }

        public override void OnBeforeRemove(NetworkPlayerCharacter entity)
        {
            entity.CanMove = true;
        }
    }*/
}