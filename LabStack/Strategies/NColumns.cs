using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LabStack.ArmyFolder;
using LabStack.Commands;
using LabStack.Units;

namespace LabStack.Strategies
{
    class NColumns : Strategy
    {
        private int _nUnitsInARow = 3;

        public NColumns(Army army1, Army army2,
            Stack<ICommand> undoCommands, Random rnd) :
            base(army1, army2, undoCommands, rnd)
        { }

        public override void ShowArmy(Army army)
        {
            Console.WriteLine($"({army.Name})");
            for (var i = 0; i < army.soldiers.Count; i += _nUnitsInARow)
            {
                Console.WriteLine($"({i} Row)");
                for (var j = 0; j < _nUnitsInARow && (i + j) < army.soldiers.Count; j++)
                    Console.Write(army.soldiers[i + j].ToString());
            }
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
            for (var i = 0; i < first.soldiers.Count && i < second.soldiers.Count; i += _nUnitsInARow)
            {
                FirstsFight(first, second, i);
                if (!(UndoCommands.Peek() is DeathCommand))
                    FirstsFight(second, first, i);
            }
        }

        protected override int HomeTerritory(RangedUnit unit, int index)
        {
            int range = index + Rnd.Next(-_nUnitsInARow, _nUnitsInARow);
            for (var i = 1; i < _nUnitsInARow; i++)
                if (index % _nUnitsInARow == i)
                    if (range > index + i || range < index - i)
                        return int.MaxValue;
            return range;
        }

        protected override int EnemyTerritory(RangedUnit unit, int index)
        {
            int addition = index % _nUnitsInARow;
            int rand = Rnd.Next(_nUnitsInARow + 2);
            if (rand >= _nUnitsInARow)
                return int.MaxValue;
            int range = index - addition + rand;
            return range;
        }

        private void RangedUnitFightEachColumn(Army first, Army second, int startIndex)
        {
            for (int i = startIndex + 1; i < startIndex + _nUnitsInARow && 
                i < first.soldiers.Count; i++)
            {
                var unit = first.soldiers[i];
                var chance = Rnd.Next(2);
                switch (chance)
                {
                    case 0:
                        if (unit is RangedUnit)
                            RangeAttack(unit as RangedUnit, i, second);
                        break;
                    case 1:
                        if (unit is Magician)
                            Clone(unit as Magician, i, first);
                        else if (unit is Healer)
                            Heal(unit as Healer, i, first);
                        else if (unit.CanDress)
                            Dress(unit as LightInfantry, i, first);
                        break;
                }
            }
        }

        protected override void RangedUnitFight(Army first, Army second)
        {
            for (var i = 0; i < first.soldiers.Count && i < second.soldiers.Count; i += _nUnitsInARow)
            {
                RangedUnitFightEachColumn(first, second, i);
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

            RangedUnitFight(first, second);
            RangedUnitFight(second, first);
        }
    }
}
