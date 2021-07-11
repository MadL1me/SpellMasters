namespace Core.GameLogic.Buffs
{
    public class WetBuff : Buff

    {
        public override string GetSpriteName() => "FireballAttack";

        public override void Use(Entity entity)
        {
            var appendBuff = false;
            foreach (var buff in entity.Buffs)
            {
                switch (buff)
                {
                    case FireBuff fireBuff:
                        // some vaporise effect
                        buff.Duration = 0;
                        Duration = 0;
                        break;
                    case WetBuff wetBuff:
                        buff.Duration += Duration;
                        break;
                    default:
                        appendBuff = true;
                        break;
                }
            }

            if (appendBuff)
                entity.Buffs.Add(this);
        }

        public override void Update(Entity entity, float deltaTime)
        {
            Duration -= deltaTime;
        }
    }
}