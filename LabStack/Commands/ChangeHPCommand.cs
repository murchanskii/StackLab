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
        private Army _enemyArmy;
        
        private List<string> _lostArmorList;
        private Armor _prevEnemyUnit;
        private int _index;
        

        public ChangeHPCommand(Unit enemyUnit, int value, Unit unit, string armyName, Army enemyArmy)
        {
            _enemyUnit = enemyUnit;
            _value = value;
            _unit = unit;
            _armyName = armyName;
            _enemyArmy = enemyArmy;
        }

        public void Undo()
        {
            _enemyUnit.TakeDamage(-_value);
            Console.Write($"({_enemyUnit.GetType().Name}[{_enemyUnit.ID}]) from ({_enemyArmy.Name}) " +
                              $"gains back HP ::{_value}::");
            if (_lostArmorList != null)
            {
                Console.WriteLine(" and his " + string.Join(",", _lostArmorList));
                _enemyArmy.soldiers.RemoveAt(_index);
                _enemyArmy.soldiers.Insert(_index, _prevEnemyUnit);
            }
            else
                Console.WriteLine("\n");
        }

        public void Do()
        {
            Console.WriteLine($"({_unit.GetType().Name}[{_unit.ID}]) from ({_armyName}) " +
                              $"attacks ({_enemyUnit.GetType().Name}[{_enemyUnit.ID}]) from ({_enemyArmy.Name}) " +
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
                    _index = _enemyArmy.soldiers.IndexOf(armoredUnit);
                    _enemyArmy.soldiers.RemoveAt(_index);
                    _enemyUnit = armoredUnit.Undress();
                    armoredUnit = _enemyUnit as Armor;
                    _enemyArmy.soldiers.Insert(_index, _enemyUnit);
                }
            }
            _enemyUnit.TakeDamage(_value);
        }
    }
}
