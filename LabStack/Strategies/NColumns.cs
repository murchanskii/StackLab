using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
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
        private List<List<Unit>> _columns1;
        private List<List<Unit>> _columns2;

        private string _army1Definition;

        public NColumns(Army army1, Army army2,
            Stack<ICommand> undoCommands, Random rnd) :
            base(army1, army2, undoCommands, rnd)
        {
            _columns1 = new List<List<Unit>>();
            InitializeColumns(army1, _columns1);
            _columns2 = new List<List<Unit>>();
            InitializeColumns(army2, _columns2);
            _army1Definition = army1.Name;
        }

        private void InitializeColumns(Army army, ICollection<List<Unit>> columns)
        {
            int j = 0;
            while (j < army.soldiers.Count)
            {
                var unitsInARow = new List<Unit>();
                for (int i = j; i < j + _nUnitsInARow && i < army.soldiers.Count; i++)
                    unitsInARow.Add(army.soldiers[i]);
                columns.Add(unitsInARow);
                j += _nUnitsInARow;
            }
        }

        public override void ShowArmy(Army army)
        {
            var i = 0;
            var columns = ConvertArmyToColumns(army);
            Console.WriteLine($"({army.Name})");
            foreach (var row in columns)
            {
                Console.WriteLine($"({i++} Row)");
                foreach (var unit in row)
                {
                    if (unit.HP > 0)
                        Console.Write(unit.ToString());
                }
            }
        }

        private List<List<Unit>> ConvertArmyToColumns(Army army)
        {
            if (String.CompareOrdinal(_army1Definition, army.Name) == 0)
                return _columns1;
            return _columns2;
        }

        private void FirstsFight(List<Unit> col1, List<Unit> col2, Army enemyArmy, int index)
        {
            var one = GetFirstAliveInColumn(col1);
            if (one == null)
                return;
            if (one is ProtectingWall)
            {
                CommandHandler(new NoActionCommand(one.GetType().Name, one.ID, one.ArmyName));
                return;
            }
            var another = GetFirstAliveInColumn(col2);
            if (another == null)
                return;
            int damage = CalcValue(one.ATK);
            CommandHandler(new ChangeHPCommand(another, damage, one, one.ArmyName, col2));
            if (another.HP <= 0)
                CommandHandler(new DeathCommand(enemyArmy.soldiers, index));
        }

        private Unit GetFirstAliveInColumn(List<Unit> column)
        {
            var first = column.First();
            int i = 1;
            while (first.HP <= 0 && i < column.Count)
                first = column.Skip(i++).First();
            if (i == 3)
                return null;
            return first;
        }

        protected override void FirstVsFirst(Army first, Army second)
        {
            var firstToColumns = ConvertArmyToColumns(first);
            var secondToColumns = ConvertArmyToColumns(second);
            for (int i = 0; i < firstToColumns.Count && i < secondToColumns.Count; i++)
            {
                FirstsFight(firstToColumns[i], secondToColumns[i], second, i * 3);
                if (!(UndoCommands.Peek() is DeathCommand))
                    FirstsFight(secondToColumns[i], firstToColumns[i], first, i * 3);
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
        }

        protected override void RangedUnitFight(Army first, Army second)
        {
            var firstToColumns = ConvertArmyToColumns(first);
            //var secondToColumns = ConvertArmyToColumns(second);
            foreach (var column in firstToColumns)
            {
                var baseCount = firstToColumns.Count;
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
                                Clone(unit as Magician, i, column);
                            else if (unit is Healer)
                                Heal(unit as Healer, i, column);
                            else if (unit.CanDress)
                                Dress(unit as LightInfantry, i, column);
                            break;
                    }
                }
            }
        }
    }
}
