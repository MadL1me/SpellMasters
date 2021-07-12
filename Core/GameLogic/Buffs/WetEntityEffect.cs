using Core.Player;

namespace Core.GameLogic.Buffs
{
    public class WetEntityEffect : EntityEffect

    {
        public override void UseOnEntity(NetworkPlayerCharacter entity)
        {
            var appendBuff = false;
            foreach (var buff in entity.PlayerEffects)
            {
                switch (buff)
                {
                    case FireEntityEffect fireBuff:
                        // some vaporise effect
                        buff.Duration = 0;
                        Duration = 0;
                        break;
                    case WetEntityEffect wetBuff:
                        buff.Duration += Duration;
                        break;
                    default:
                        appendBuff = true;
                        break;
                }
            }

            if (appendBuff)
                entity.PlayerEffects.Add(this);
        }
    }
}