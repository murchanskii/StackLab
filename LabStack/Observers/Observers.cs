using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Observers
{
    interface IObserver
    {
        void Update(int id, string armyName);
    }

    interface IObservable
    {
        void AddObserver(IObserver obs);
        void RemoveObserver(IObserver obs);
    }
}
