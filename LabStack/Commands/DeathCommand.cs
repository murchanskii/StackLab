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
        private Army _army;
        private int _index;
        private List<Unit> _tempSoldiers;
        public DeathCommand(Army army, int index)
        {
            _army = army;
            _index = index;
        }

        public void Undo()
        {
            _army.soldiers = _tempSoldiers;
            Console.WriteLine($"({_army.soldiers[_index].GetType().Name}[{_index}] from ({_army.Name}) " +
                              $"became alive)");
            string[] lines = File.ReadAllLines(@"deaths.txt");
            File.WriteAllLines(@"deaths.txt", lines.Take(lines.Count() - 1));
        }

        public void Do()
        {
            _army.soldiers[_index].ReportDeadUnit();
            _tempSoldiers = _army.soldiers.ToList();
            _army.soldiers.RemoveAt(_index);
        }
    }
}
