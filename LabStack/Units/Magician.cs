﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack.Units
{
    sealed class Magician : RangedUnit
    {
        public Magician(Random rnd, int id, string armyName)
        {
            MaxHP = HP = 60;
            ATK = rnd.Next(10, 15);
            DEF = rnd.Next(10, 13);
            RangedAbility = rnd.Next(15, 23); // range atk as archer
            Range = rnd.Next(1, 5); // range as archer
            Cost = Costs.Magician;
            ID = id;
            ArmyName = armyName;
        }

        public void Clone(List<Unit> army, IClonable unit, bool isUndo)
        {
            if (isUndo)
            {
                army.RemoveAt(army.Count - 1);
                return;
            }
            army.Add(unit.Clone() as Unit);
        }
    }
}
