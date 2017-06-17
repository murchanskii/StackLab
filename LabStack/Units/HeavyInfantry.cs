using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using LabStack.Units.Armors;

namespace LabStack.Units
{
    sealed class HeavyInfantry : Unit
    {
        public HeavyInfantry(Random rnd, int id, string armyName)
        {
            MaxHP = HP = 60;
            ATK = rnd.Next(30, 38);
            DEF = rnd.Next(20, 25);
            Cost = Costs.HeavyInfantry;
            ID = id;
            ArmyName = armyName;
            CanBeDressed = true;
        }
    }
}
