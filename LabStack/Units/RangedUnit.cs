using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Units
{
    abstract class RangedUnit : Unit
    {
        public int RangedAbility { get; protected set; } // сила атаки/хила
        public int Range { get; protected set; } // дистанция абилити

        public override string ToString()
        {
            return $" ({GetType().Name}[{ID}]): HP: {HP}; ATK: {ATK}; " +
                   $"RangedAbility: {RangedAbility}; Range: {Range}; DEF: {DEF}; Cost: {Cost};\n";
        }
    }
}
