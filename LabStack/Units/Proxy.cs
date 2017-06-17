using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Units
{
    sealed class Proxy : Unit
    {
        private Archer _archer;
        private static string _logFile = @"archer_log.txt";
        private StreamWriter sw;

        public Proxy(Archer a, int id, string armyName)
        {
            _archer = a;
            MaxHP = HP = _archer.HP;
            ATK = _archer.ATK;
            DEF = _archer.DEF;
            Cost = _archer.Cost;
            ID = _archer.ID;
            ArmyName = _archer.ArmyName;
        }

        public override void TakeDamage(int damage)
        {
            _archer.TakeDamage(damage);
            if (damage < 0)
                lock (sw = new StreamWriter(_logFile, true, Encoding.Default))
                {
                    sw.WriteLine($"({_archer.GetType().Name}[{ID}]) from ({ArmyName}) gains HP ::{-damage}::");
                    sw.Close();
                }
            else
                lock (sw = new StreamWriter(_logFile, true, Encoding.Default))
                {
                    sw.WriteLine($"({_archer.GetType().Name}[{ID}]) from ({ArmyName}) gets [{damage}] damage");
                    sw.Close();
                }
            
        }

        public override void ReportDeadUnit()
        {
            NotifyObservers(ID, ArmyName);
            lock (sw = new StreamWriter(_logFile, true, Encoding.Default))
            {
                sw.WriteLine($"({_archer.GetType().Name}[{ID}]) from ({ArmyName}) is dead");
                sw.Close();
            }
        }

        public override string ToString()
        {
            return $" ({_archer.GetType().Name}[{ID}]): HP: {HP}; ATK: {ATK}; " +
                   $"DEF: {DEF}; Cost: {Cost};\n";
        }
    }
}
