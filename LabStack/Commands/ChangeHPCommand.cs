using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using LabStack.ArmyFolder;
using LabStack.Units;
using LabStack.Units.Armors;

namespace LabStack.Commands
{
    class ChangeHPCommand : ICommand
    {
        private Unit _enemyUnit;
        private readonly Unit _unit;
        private int _value;
        private readonly string _armyName;
        private List<Unit> _enemyUnits;
        
        private List<string> _lostArmorList;
        private Armor _prevEnemyUnit;
        private int _index;
        

        public ChangeHPCommand(Unit enemyUnit, int value, Unit unit, string armyName, List<Unit> enemyUnits)
        {
            _enemyUnit = enemyUnit;
            _value = value;
            _unit = unit;
            _armyName = armyName;
            _enemyUnits = enemyUnits;
        }

        public void Undo()
        {
            _enemyUnit.TakeDamage(-_value);
            Console.Write($"({_enemyUnit.GetType().Name}[{_enemyUnit.ID}]) from ({_enemyUnit.ArmyName}) " +
                              $"gains back HP ::{_value}::");
            if (_lostArmorList != null)
            {
                Console.WriteLine(" and his " + string.Join(",", _lostArmorList));
                _enemyUnits.RemoveAt(_index);
                _enemyUnits.Insert(_index, _prevEnemyUnit);
            }
            else
                Console.WriteLine("\n");
        }

        public void Do()
        {
            Console.WriteLine($"({_unit.GetType().Name}[{_unit.ID}]) from ({_armyName}) " +
                              $"attacks ({_enemyUnit.GetType().Name}[{_enemyUnit.ID}]) from ({_enemyUnit.ArmyName}) " +
                              $"with ::{_value}::");
            if (_enemyUnit is Armor)
            {
                var armoredUnit = (Armor) _enemyUnit;
                _prevEnemyUnit = armoredUnit;
                while (armoredUnit != null && _value > 0 && armoredUnit.armors.Count > 0 && _value >= armoredUnit.DEFbuff)
                {
                    _lostArmorList = new List<string>();
                    Console.WriteLine($"(HeavyInfantry[{armoredUnit.ID}]) from ({armoredUnit.ArmyName})" +
                                      $" loses his |{armoredUnit.armors.Last()}|");
                    _lostArmorList.Add(armoredUnit.armors.Last());
                    _value -= armoredUnit.DEFbuff;
                    _index = _enemyUnits.IndexOf(armoredUnit);
                    _enemyUnits.RemoveAt(_index);
                    _enemyUnit = armoredUnit.Undress();
                    armoredUnit = _enemyUnit as Armor;
                    _enemyUnits.Insert(_index, _enemyUnit);
                }
            }
            _enemyUnit.TakeDamage(_value);
        }
    }
}
