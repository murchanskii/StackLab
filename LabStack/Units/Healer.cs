using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Units
{
    sealed class Healer : RangedUnit
    {
        public Healer(Random rnd, int id, string armyName)
        {
            MaxHP = HP = 50;
            ATK = rnd.Next(5, 10);
            DEF = rnd.Next(10, 13);
            RangedAbility = rnd.Next(10, 18);
            Range = rnd.Next(1, 5);
            Cost = Costs.Healer;
            ID = id;
            ArmyName = armyName;
        }

        public int Heal(Unit unit, int value)
        {
            unit.TakeDamage(-value);
            int defWas = unit.DEF;
            unit.DEF = 0;
            return defWas;
        }
    }
}
