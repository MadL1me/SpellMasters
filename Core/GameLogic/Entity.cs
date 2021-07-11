using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.GameLogic
{
    public class Entity
    {
        public List<Buff> Buffs = new List<Buff>();

        public Stamina Stamina = new Stamina(0);

        public float Health;

        public void UseBuff(Buff buff)
        {
            buff.Use(this);
        }

        public void Cleanup()
        {
            Buffs = Buffs.Where(buff => buff.Duration > 0).ToList();
        }

    }
}