using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Units.Armors
{
    abstract class Armor : Unit
    {
        private Unit _unit;
        public List<string> armors = new List<string>();
        public override int ATK => _unit.ATK + ATKbuff;

        public int DEFbuff;
        public int ATKbuff;

        public override int DEF
        {
            get => _unit.DEF + DEFbuff;
            set => _unit.DEF = value;
        }

        public override int HP
        {
            get => _unit.HP;
            set => _unit.HP = value;
        }
        public override int MaxHP => _unit.MaxHP;
        public override int Cost => _unit.Cost;
        public override bool IsHealable => _unit.IsHealable;
        public override bool CanDress => _unit.CanDress;
        public override bool CanBeDressed => _unit.CanBeDressed;
        public override int ID => _unit.ID;
        public override string ArmyName => _unit.ArmyName;


        public Armor(Unit unit)
        {
            _unit = unit;
            var dressableUnit = unit as Armor;
            if (dressableUnit != null)
                armors.AddRange(dressableUnit.armors);
            armors.Add(GetType().Name);
        }

        public Unit Undress()
        {
            return _unit;
        }

        public override string ToString()
        {
            if (armors.Count == 0)
                return $" (HeavyInfantry[{ID}]): HP: {HP}; ATK: {ATK}; DEF: {DEF}; Cost: {Cost};\n";
            return $" (HeavyInfantry[{ID}]): HP: {HP}; ATK: {ATK}; DEF: {DEF}; Cost: {Cost};\n" +
                   "  " + string.Join(" | ", armors) + "\n";
        }
    }
}
