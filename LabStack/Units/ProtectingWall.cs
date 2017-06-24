using System;
using SpecialUnits;

namespace LabStack.Units
{
    sealed class ProtectingWall : Unit
    {
        private GulyayGorod _wall;

        public ProtectingWall(Random rnd, int id, string armyName)
        {
            ATK = 0;
            IsHealable = false;
            ID = id;
            _wall = new GulyayGorod(100, rnd.Next(30, 41), Costs.Wall);
            ArmyName = armyName;
        }

        private int _lastHP;

        public override int HP => _wall.GetCurrentHealth();
        public override int ATK => _wall.GetStrength();
        public override int Cost => _wall.GetCost();
        public override int DEF => _wall.GetDefence();

        public override void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                _wall = new GulyayGorod(HP + _lastHP, DEF, Cost);
            }
            else
            {
                _lastHP = HP;
                _wall.TakeDamage(damage + DEF);
            }
        }

        public override string ToString()
        {
            return $" ({GetType().Name}[{ID}]): HP: {HP}; DEF: {DEF}; Cost: {Cost}\n";
        }
    }
}
