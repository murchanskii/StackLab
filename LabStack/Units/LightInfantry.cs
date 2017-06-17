using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabStack.ArmyFolder;
using LabStack.Units.Armors;

namespace LabStack.Units
{
    sealed class LightInfantry : Unit, IClonable
    {
        private Random _rnd;
        public LightInfantry(Random rnd, int id, string armyName)
        {
            MaxHP = HP = 60;
            ATK = rnd.Next(20, 25);
            DEF = rnd.Next(10, 13);
            Cost = Costs.LightInfantry;
            ID = id;
            ArmyName = armyName;
            if (rnd.Next(2) == 1)
                CanDress = true;
            _rnd = rnd;
        }

        public override string ToString()
        {
            return $" ({GetType().Name}[{ID}]): HP: {HP}; ATK: {ATK}; DEF: {DEF}; Cost: {Cost}; Can dress: {CanDress}\n";
        }

        public void Dress(List<Unit> units, int index, Unit armor)
        {
            units.RemoveAt(index);
            units.Insert(index, armor);
        }

        public LightInfantry(int hp, int atk, int def, int cost, int id, string armyName)
        {
            MaxHP = HP = hp;
            ATK = atk;
            DEF = def;
            Cost = cost;
            ID = id + 100;
            ArmyName = armyName;
        }

        public IClonable Clone()
        {
            return new LightInfantry(HP, ATK, DEF, Cost, ID, ArmyName);
        }
    }
}
