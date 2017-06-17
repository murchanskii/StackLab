using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Units.Armors
{
    class LongSword : Armor
    {

        public LongSword(Unit unit) : base(unit)
        {
            ATKbuff = 10;
        }
    }
}
