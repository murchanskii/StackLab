using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Commands
{
    class NoActionCommand : ICommand
    {
        private string _unit;
        private int _id;
        private string _armyName;
        public NoActionCommand(string unit, int id, string armyName)
        {
            _unit = unit;
            _id = id;
            _armyName = armyName;
        }

        public void Undo()
        {
            Console.WriteLine("Nothing actually happened here");
        }

        public void Do()
        {
            if (_unit.Contains("Wall"))
            {
                Console.WriteLine($"({_unit}[{_id}]) from ({_armyName}) just stands here");
                return;
            }
            Console.WriteLine($"({_unit}[{_id}]) from ({_armyName}) missed");
        }

    }
}
