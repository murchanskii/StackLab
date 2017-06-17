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
        private Army _army;
        private Unit _armor;
        public GetDressedCommand(LightInfantry l, Army armyToDress, int indexOfHeavy, Unit armor)
        {
            _light = l;
            _index = indexOfHeavy;
            _army = armyToDress;
            _armor = armor;
        }

        public void Undo()
        {
            _army.soldiers.RemoveAt(_index);
            Armor armor;
            if ((armor = _armor as Armor) != null)
                _army.soldiers.Insert(_index, armor.Undress());
            Console.WriteLine($"({_light.GetType().Name}[{_light.ID}]) from ({_light.ArmyName})" +
                              $" took off |{_armor.GetType().Name}| from " +
                              $"(HeavyInfantry[{_army.soldiers[_index].ID}])");
        }

        public void Do()
        {
            _light.Dress(_army.soldiers, _index, _armor);
            Console.WriteLine($"({_light.GetType().Name}[{_light.ID}]) from ({_light.ArmyName})" +
                              $" dressed (HeavyInfantry[{_army.soldiers[_index].ID}]) with" +
                              $" |{_armor.GetType().Name}|");
        }
    }
}
