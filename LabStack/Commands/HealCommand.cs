using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabStack.Units;

namespace LabStack.Commands
{
    class HealCommand : ICommand
    {
        private Unit _unit;
        private Healer _healer;
        private int _value;
        private string _armyName;
        private int _def;

        public HealCommand(Healer healer, Unit unit, int value, string armyName)
        {
            _unit = unit;
            _healer = healer;
            _value = value;
            _armyName = armyName;
        }

        public void Undo()
        {
            _unit.TakeDamage(_value - _def);
            Console.WriteLine($"({_healer.GetType().Name}[{_healer.ID}]) from ({_armyName}) " +
                              $"takes HP ::{_value}:: back from " +
                              $"({_unit.GetType().Name}[{_unit.ID}]) from ({_armyName})");
        }

        public void Do()
        {
            if (_unit.IsHealable)
            {
                _def = _healer.Heal(_unit, _value);
                Console.WriteLine($"({_healer.GetType().Name}[{_healer.ID}]) from ({_armyName}) " +
                                  $"heals ({_unit.GetType().Name}[{_unit.ID}]) from ({_armyName}) " +
                                  $"with ::{_value}::");
            }
        }
    }
}
