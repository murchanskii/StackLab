using System;
using SpecialUnits;

namespace LabStack.Units
{
    sealed class ProtectingWall : Unit
    {
        private readonly GulyayGorod _wall;

        public ProtectingWall(Random rnd, int id, string armyName)
        {
            MaxHP = HP = 100;
            ATK = 0;
            DEF = rnd.Next(30, 41);
            Cost = Costs.Wall;
            IsHealable = false;
            ID = id;
            _wall = new GulyayGorod(MaxHP, DEF, Cost);
            ArmyName = armyName;
        }

        public override void TakeDamage(int damage)
        {
            if (damage < 0)
                HP -= damage;
            else
            {
                _wall.TakeDamage(damage + DEF);
                HP -= damage;
            }
        }

        public override string ToString()
        {
            return $" ({GetType().Name}[{ID}]): HP: {HP}; DEF: {DEF}; Cost: {Cost}\n";
        }
    }
}
