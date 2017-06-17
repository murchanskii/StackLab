using System;
using System.Collections.Generic;
using System.Linq;
using LabStack.ArmyFolder;
using LabStack.Commands;
using LabStack.Units;

namespace LabStack.Strategies
{
    class OneColumn : Strategy
    {
        public OneColumn(Army army1, Army army2, 
            Stack<ICommand> undoCommands, Random rnd) :
            base (army1, army2, undoCommands, rnd)
        { }

        public override void ShowArmy(Army army)
        {
            Console.WriteLine(army.ToString());
        }

        protected override void FirstVsFirst(Army first, Army second)
        {
            var one = first.soldiers.First();
            if (one is ProtectingWall)
            {
                CommandHandler(new NoActionCommand(one.GetType().Name, one.ID, first.Name));
                return;
            }
            var another = second.soldiers.First();

            int damage = CalcValue(one.ATK);
            CommandHandler(new ChangeHPCommand(another, damage, one, one.ArmyName, second.soldiers));
            if (another.HP <= 0)
                CommandHandler(new DeathCommand(second.soldiers, 0));
        }

        protected override void RangedUnitFight(Army first, Army second)
        {
            var baseCount = first.soldiers.Count;
            for (int i = 1; i < baseCount; i++)
            {
                var unit = first.soldiers[i];
                var chance = Rnd.Next(2);
                switch (chance)
                {
                    case 0:
                        if (unit is RangedUnit)
                            RangeAttack(unit as RangedUnit, i, second.soldiers);
                        break;
                    case 1:
                        if (unit is Magician)
                            Clone(unit as Magician, i, first.soldiers);
                        else if (unit is Healer)
                            Heal(unit as Healer, i, first.soldiers);
                        else if (unit.CanDress)
                            Dress(unit as LightInfantry, i, first.soldiers);
                        break;
                }
            }
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

            RangedUnitFight(first, second);
            RangedUnitFight(second, first);

        }
    }
}
