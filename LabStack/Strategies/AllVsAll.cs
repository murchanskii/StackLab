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
            throw new NotImplementedException();
        }

        protected override void FirstVsFirst(Army first, Army second)
        {
            throw new NotImplementedException();
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
            if (!(UndoCommands.Peek() is DeathCommand))
                FirstVsFirst(second, first);
        }
    }
}
