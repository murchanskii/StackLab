using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabStack.ArmyFolder;
using LabStack.Units;

namespace LabStack.Commands
{
    class DeathCommand : ICommand
    {
        private List<Unit> _units;
        private int _index;
        private List<Unit> _tempSoldiers;
        public DeathCommand(List<Unit> units, int index)
        {
            _units = units;
            _index = index;
        }

        public void Undo()
        {
            _units = _tempSoldiers;
            Console.WriteLine($"({_units[_index].GetType().Name}[{_index}] from ({_units[_index].ArmyName}) " +
                              $"became alive)");
            string[] lines = File.ReadAllLines(@"deaths.txt");
            File.WriteAllLines(@"deaths.txt", lines.Take(lines.Count() - 1));
        }

        public void Do()
        {
            _units[_index].ReportDeadUnit();
            _tempSoldiers = _units.ToList();
            _units.RemoveAt(_index);
        }
    }
}
