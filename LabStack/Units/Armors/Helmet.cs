using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Units.Armors
{
    class Helmet : Armor
    {
        public Helmet(Unit unit) : base(unit)
        {
            DEFbuff = 5;
        }
    }
}
