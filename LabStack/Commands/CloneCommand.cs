using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabStack.ArmyFolder;
using LabStack.Units;

namespace LabStack.Commands
{
    class CloneCommand : ICommand
    {
        private Magician _magician;
        private Army _army;
        private IClonable _unit;
        public CloneCommand(Magician magician, Army army, IClonable unit)
        {
            _magician = magician;
            _army = army;
            _unit = unit;
        }

        public void Undo()
        {
            Console.WriteLine($"({_unit.GetType().Name}[{(_unit as Unit).ID}]) from ({(_unit as Unit).ArmyName}) " +
                                $"loses his clone at [{_army.soldiers.Count}]");
            _magician.Clone(_army, _unit, true);
        }

        public void Do()
        {
            _magician.Clone(_army, _unit, false);
            Console.WriteLine($"({_magician.GetType().Name}[{_magician.ID}]) from ({_magician.ArmyName}) " +
                              $"clones ({_unit.GetType().Name}[{(_unit as Unit).ID}]) from ({(_unit as Unit).ArmyName}) " +
                              $"to [{_army.soldiers.Count}]");
        }
    }
}
