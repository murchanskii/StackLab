using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LabStack.ArmyFolder;
using LabStack.Units;
using LabStack.Units.Armors;

namespace LabStack.Commands
{
    class GetDressedCommand : ICommand
    {
        private LightInfantry _light;
        private int _index;
        private List<Unit> _units;
        private Unit _armor;
        public GetDressedCommand(LightInfantry l, List<Unit> unitsToDress, int indexOfHeavy, Unit armor)
        {
            _light = l;
            _index = indexOfHeavy;
            _units = unitsToDress;
            _armor = armor;
        }

        public void Undo()
        {
            _units.RemoveAt(_index);
            Armor armor;
            if ((armor = _armor as Armor) != null)
                _units.Insert(_index, armor.Undress());
            Console.WriteLine($"({_light.GetType().Name}[{_light.ID}]) from ({_light.ArmyName})" +
                              $" took off |{_armor.GetType().Name}| from " +
                              $"(HeavyInfantry[{_units[_index].ID}])");
        }

        public void Do()
        {
            _light.Dress(_units, _index, _armor);
            Console.WriteLine($"({_light.GetType().Name}[{_light.ID}]) from ({_light.ArmyName})" +
                              $" dressed (HeavyInfantry[{_units[_index].ID}]) with" +
                              $" |{_armor.GetType().Name}|");
        }
    }
}
