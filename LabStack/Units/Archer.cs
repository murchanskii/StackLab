using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Units
{
    sealed class Archer : RangedUnit, IClonable
    {
        public Archer(Random rnd, int id, string armyName)
        {
            MaxHP = HP = 50;
            ATK = rnd.Next(10, 15);
            DEF = rnd.Next(10, 13);
            RangedAbility = rnd.Next(15, 23);
            Range = rnd.Next(1, 5);
            Cost = Costs.Archer;
            ID = id;
            ArmyName = armyName;
        }

        public Archer(int hp, int atk, int def, int rangedAtk, int range, int cost, int id, string armyName)
        {
            MaxHP = HP = hp;
            ATK = atk;
            DEF = def;
            RangedAbility = rangedAtk;
            Range = range;
            Cost = cost;
            ID = id + 100;
            ArmyName = armyName;
        }

        public IClonable Clone()
        {
            return new Archer(HP, ATK, DEF, RangedAbility, Range, Cost, ID, ArmyName);
        }
    }
}
