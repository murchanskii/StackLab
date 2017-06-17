using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabStack.Observers;

namespace LabStack.Units
{
    static class Costs
    {
        public static int LightInfantry { get; } = 25;
        public static int HeavyInfantry { get; } = 45;
        public static int Archer { get; } = 35;
        public static int Magician { get; } = 40;
        public static int Healer { get; } = 50;
        public static int Wall { get; } = 80;
    }

    abstract class Unit : IObservable
    {
        protected List<IObserver> observers;
        public virtual int ID { get; protected set; }
        public virtual string ArmyName { get; protected set; }

        public virtual int MaxHP { get; protected set; }
        public virtual int HP { get; set; }
        public virtual int ATK { get; protected set; }
        public virtual int DEF { get; set; }
        public virtual int Cost { get; protected set; }
        public virtual bool IsHealable { get; protected set; } = true;
        public virtual bool CanDress { get; protected set; } = false;
        public virtual bool CanBeDressed { get; protected set; } = false;

        public Unit()
        {
            observers = new List<IObserver>();
            observers[0] = new Annalist(this);
            observers[1] = new Kill(this);
        }

        public override string ToString()
        {
            return $" ({GetType().Name}[{ID}]): HP: {HP}; ATK: {ATK}; DEF: {DEF}; Cost: {Cost};\n";
        }

        public virtual void TakeDamage(int damage)
        {
            if (damage < 0)
                AddHP(-damage);
            else
            {
                DEF -= damage;
                if (DEF < 0)
                {
                    HP += DEF;
                    DEF = 0;
                }
            }
        }

        private void AddHP(int value)
        {
            int delta = MaxHP - HP;
            if (value > delta)
            {
                HP = MaxHP;
                DEF += value - delta;
            }
            else
                HP += value;
        }

        public virtual void ReportDeadUnit()
        {
            NotifyObservers(ID, ArmyName);
        }

        public void AddObserver(IObserver obs)
        {
            observers.Add(obs);
        }

        public void RemoveObserver(IObserver obs)
        {
            observers.Remove(obs);
        }

        protected void NotifyObservers(int index, string armyName)
        {
            foreach (IObserver observer in observers)
                observer.Update(index, armyName);
        }
    }
}
