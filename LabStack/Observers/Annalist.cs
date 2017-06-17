using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Observers
{
    class Annalist : IObserver // writes to file
    {
        private IObservable unit;
        public Annalist(IObservable obs)
        {
            unit = obs;
            unit.AddObserver(this);
        }

        public void Update(int id, string armyName)
        {
            var typeOfUnit = unit.GetType().Name;
            var deathBook = @"deaths.txt";
            try
            {
                using (StreamWriter sw =
                    new StreamWriter(deathBook, true, Encoding.Default))
                {
                    sw.WriteLine($"({typeOfUnit}[{id}]) from ({armyName}) is dead");
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
