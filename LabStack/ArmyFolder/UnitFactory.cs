using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabStack.Units;

namespace LabStack.ArmyFolder
{
    interface IFactory
    {
        Unit CreateUnit(ref int cost, int id, string armyName);
    }

    class UnitFactory : IFactory
    {
        private Random rnd = new Random();

        //public Unit CreateUnit(ref int cost, int id, string armyName)
        //{
        //    if (cost >= Costs.HeavyInfantry && rnd.Next(4) <= 1)
        //    {
        //        cost -= Costs.HeavyInfantry;
        //        return new HeavyInfantry(rnd, id, armyName);
        //    }
        //    if (cost >= Costs.LightInfantry)
        //    {
        //        cost -= Costs.LightInfantry;
        //        return new LightInfantry(rnd, id, armyName);
        //    }
        //    return null;
        //}

        public Unit CreateUnit(ref int cost, int id, string armyName)
        {
            double rnd_prcnt = rnd.Next(1, 118) * 0.01;
            // algorithm for rand get unit
            if (rnd_prcnt <= 0.475 && rnd_prcnt > 0.3)
            {
                if (cost >= Costs.HeavyInfantry)
                {
                    cost -= Costs.HeavyInfantry;
                    return new HeavyInfantry(rnd, id, armyName);
                }
            }
            else if (rnd_prcnt <= 0.65 && rnd_prcnt > 0.475)
            {
                if (cost >= Costs.Archer)
                {
                    cost -= Costs.Archer;
                    return new Archer(rnd, id, armyName);
                }
            }
            else if (rnd_prcnt <= 0.825 && rnd_prcnt > 0.65)
            {
                if (cost >= Costs.Magician)
                {
                    cost -= Costs.Magician;
                    return new Magician(rnd, id, armyName);
                }
            }
            else if (rnd_prcnt <= 1 && rnd_prcnt > 0.825)
            {
                if (cost >= Costs.Healer)
                {
                    cost -= Costs.Healer;
                    return new Proxy(new Healer(rnd, id, armyName));
                }
            }
            else if (rnd_prcnt <= 1.18 && rnd_prcnt > 1)
            {
                if (cost >= Costs.Wall)
                {
                    cost -= Costs.Wall;
                    return new ProtectingWall(rnd, id, armyName);
                }
            }
            if (cost >= Costs.LightInfantry)
            {
                cost -= Costs.LightInfantry;
                return new LightInfantry(rnd, id, armyName);
            }
            return null;
        }
    }
}
