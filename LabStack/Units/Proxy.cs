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
        public override int ATK  => _healer.ATK;
        public override int HP { get => _healer.HP; set => _healer.HP = value; }
        public override string ArmyName => _healer.ArmyName;
        public override bool CanBeDressed => _healer.CanBeDressed;
        public override bool CanDress => _healer.CanDress;
        public override int Cost => _healer.Cost;
        public override int DEF { get => _healer.DEF; set => _healer.DEF = value; }
        public override int ID => _healer.ID;
        public override bool IsHealable => _healer.IsHealable;
        public override int MaxHP => _healer.MaxHP;

        private Healer _healer;
        private static string _logFile = @"archer_log.txt";
        private StreamWriter sw;

        public Proxy(Healer h)
        {
            _healer = h;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (damage < 0)
                lock (sw = new StreamWriter(_logFile, true, Encoding.Default))
                {
                    sw.WriteLine($"({_healer.GetType().Name}[{ID}]) from ({ArmyName}) gains HP ::{-damage}::");
                    sw.Close();
                }
            else
                lock (sw = new StreamWriter(_logFile, true, Encoding.Default))
                {
                    sw.WriteLine($"({_healer.GetType().Name}[{ID}]) from ({ArmyName}) gets [{damage}] damage");
                    sw.Close();
                }
        }

        public override void ReportDeadUnit()
        {
            NotifyObservers(ID, ArmyName);
            lock (sw = new StreamWriter(_logFile, true, Encoding.Default))
            {
                sw.WriteLine($"({_healer.GetType().Name}[{ID}]) from ({ArmyName}) is dead");
                sw.Close();
            }
        }

        public override string ToString()
        {
            return $" ({_healer.GetType().Name}[{ID}]): HP: {HP}; ATK: {ATK}; " +
                   $"DEF: {DEF}; Cost: {Cost};\n";
        }
    }
}
