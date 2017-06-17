using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Units.Armors
{
    class Shield : Armor
    {
        public Shield(Unit unit) : base(unit)
        {
            DEFbuff = 10;
        }
    }
}
