using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Observers
{
    class Kill : IObserver
    {
        private IObservable unit;

        public Kill(IObservable obs)
        {
            unit = obs;
            unit.AddObserver(this);
        }

        public void Update(int id, string armyName)
        {
            Console.Beep();
            var typeOfUnit = unit.GetType().Name;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"({typeOfUnit}[{id}]) from ({armyName}) is dead");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
