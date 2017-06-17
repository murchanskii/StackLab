using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabStack.ArmyFolder;
using LabStack.Commands;
using LabStack.Units;
using LabStack.Units.Armors;

namespace LabStack.Strategies
{
    abstract class Strategy
    {
        protected Army Army1;
        protected Army Army2;
        protected Random Rnd;

        protected Stack<ICommand> UndoCommands;

        protected Strategy(Army army1, Army army2,
            Stack<ICommand> undoCommands, Random rnd)
        {
            Army1 = army1;
            Army2 = army2;
            UndoCommands = undoCommands;
            Rnd = rnd;
        }

        protected abstract void FirstVsFirst(Army first, Army second);
        protected abstract void RangedUnitFight(Army first, Army second);
        protected abstract int HomeTerritory(RangedUnit unit, int index);
        protected abstract int EnemyTerritory(RangedUnit unit, int index);

        protected void Heal(Healer healer, int index, Army army)
        {
            int force = CalcValue(healer.RangedAbility);
            int range = HomeTerritory(healer, index);
            if (range < 0 || range >= army.soldiers.Count)
                CommandHandler(new NoActionCommand(healer.GetType().Name, index, healer.ArmyName));
            else
                CommandHandler(new HealCommand(healer, army.soldiers[range], force, army.Name));
        }

        protected void Clone(Magician magician, int index, Army army)
        {
            int range = HomeTerritory(magician, index);
            IClonable unitToClone;
            if (range < 0 || range >= army.soldiers.Count || (unitToClone = army.soldiers[range] as IClonable) == null)
                CommandHandler(new NoActionCommand(magician.GetType().Name, index, magician.ArmyName));
            else
                CommandHandler(new CloneCommand(magician, army, unitToClone));
        }

        protected void Dress(LightInfantry unit, int index, Army army)
        {
            int range = index + Rnd.Next(-1, 2);
            if (range >= 0 && range < army.soldiers.Count)
                if (army.soldiers[range].CanBeDressed)
                {
                    int chance = Rnd.Next(4);
                    var dressableUnit = army.soldiers[range];
                    Unit armor = new Helmet(dressableUnit);
                    switch (chance)
                    {
                        case 0:
                            armor = new Horse(dressableUnit);
                            break;
                        case 1:
                            armor = new LongSword(dressableUnit);
                            break;
                        case 2:
                            armor = new Shield(dressableUnit);
                            break;
                    }
                    var armorA = (Armor)armor;
                    if (!armorA.armors.GetRange(0, armorA.armors.Count - 1).Contains(armor.GetType().Name))
                        CommandHandler(new GetDressedCommand(unit, army, range, armor));
                    else
                        CommandHandler(new NoActionCommand(unit.GetType().Name, index, unit.ArmyName));
                }
        }

        protected void RangeAttack(RangedUnit archer, int index, Army enemyArmy)
        {
            int damage = CalcValue(archer.RangedAbility);
            int range = EnemyTerritory(archer, index);
            if (range < 0 || range >= enemyArmy.soldiers.Count)
                CommandHandler(new NoActionCommand(archer.GetType().Name, index, archer.ArmyName));
            else
            {
                CommandHandler(new ChangeHPCommand(enemyArmy.soldiers[range], damage, archer, archer.ArmyName,
                    enemyArmy));
                if (enemyArmy.soldiers[range].HP <= 0)
                    CommandHandler(new DeathCommand(enemyArmy, range));
            }
        }

        protected void CommandHandler(ICommand cmd)
        {
            var command = cmd;
            UndoCommands.Push(command);
            command.Do();
        }
        
        protected int CalcValue(int value)
        {
            value += Rnd.Next(-3, 4);
            if (Rnd.Next(16) == 1)
                value *= 2;
            return value;
        }

        public virtual void Turn()
        {
            var first = Army1;
            var second = Army2;
            if (Rnd.Next(1, 3) == 2)
            {
                var t = first;
                first = second;
                second = t;
            }

            FirstVsFirst(first, second);
            if (!(UndoCommands.Peek() is DeathCommand))
                FirstVsFirst(second, first);

            RangedUnitFight(first, second);
            RangedUnitFight(second, first);
        }
    }
}
