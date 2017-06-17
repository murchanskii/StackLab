using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabStack.ArmyFolder;
using LabStack.Commands;
using LabStack.Units;

namespace LabStack.Strategies
{
    class AllVsAll : Strategy
    {
        public AllVsAll(Army army1, Army army2,
            Stack<ICommand> undoCommands, Random rnd) :
            base(army1, army2, undoCommands, rnd)
        { }

        public override void ShowArmy(Army army)
        {
            Console.WriteLine(army.ToString());
        }

        private void FirstsFight(Army first, Army second, int index)
        {
            var one = first.soldiers[index];
            if (one is ProtectingWall)
            {
                CommandHandler(new NoActionCommand(one.GetType().Name, one.ID, first.Name));
                return;
            }
            var another = second.soldiers[index];
            int damage = CalcValue(one.ATK);
            CommandHandler(new ChangeHPCommand(another, damage, one, one.ArmyName, second));
            if (another.HP <= 0)
                CommandHandler(new DeathCommand(second, index));
        }

        protected override void FirstVsFirst(Army first, Army second)
        {
            for (int i = 0; i < first.soldiers.Count && i < second.soldiers.Count; i++)
            {
                FirstsFight(first, second, i);
                if (!(UndoCommands.Peek() is DeathCommand))
                    FirstsFight(second, first, i);
            }
        }

        protected override void RangedUnitFight(Army first, Army second)
        {
            throw new NotImplementedException();
        }

        public override void Turn()
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
        }
    }
}
