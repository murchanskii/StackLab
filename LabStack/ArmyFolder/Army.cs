using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabStack.Units;

namespace LabStack.ArmyFolder
{
    class Army
    {
        public List<Unit> soldiers;
        public string Name { get; }
        public Army(string name, int cost)
        {
            Name = name;
            var units = new UnitFactory();
            soldiers = new List<Unit>();
            while (cost >= Costs.LightInfantry)
                soldiers.Add(units.CreateUnit(ref cost, soldiers.Count, Name));
        }
        
        public override string ToString()
        {
            return $"( {Name} )\n {string.Join(" ", soldiers.Select(unit => unit.ToString()))}";
        }
    }
}
