using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabStack.Strategies;

namespace LabStack
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = Engine.Instance;
            int a = 0;
            while (a != 7)
            {
                Console.WriteLine("Menu");
                Console.WriteLine("0. Create soldiers");
                Console.WriteLine("1. Show armies");
                Console.WriteLine("2. Next turn");
                Console.WriteLine("3. Undo turn");
                Console.WriteLine("4. Redo turn");
                Console.WriteLine("5. Play game until end");
                Console.WriteLine("6. Set Strategy");
                Console.WriteLine("7. Exit");
                Console.Write("Enter menu item: ");
                while (!int.TryParse(Console.ReadLine(), out a));
                switch (a)
                {
                    case 0:
                        game.CreateArmy();
                        break;
                    case 1:
                        game.ShowArmies();
                        break;
                    case 2:
                        // turn
                        game.Turn();
                        break;
                    case 3:
                        // undo
                        game.Undo();
                        break;
                    case 4:
                        // redo
                        game.Redo();
                        break;
                    case 5:
                        // until end
                        game.DoTurnUntilEnd();
                        break;
                    case 6:
                        // choose strategy
                        Console.WriteLine("  Enter number of preferred strategy");
                        Console.WriteLine("  0. One by one");
                        Console.WriteLine("  1. N rows VS M rows");
                        Console.WriteLine("  2. All VS All");
                        int num;
                        while (!int.TryParse(Console.ReadLine(), out num)) ;
                        if (num < 0 || num > 2)
                        {
                            Console.WriteLine("No such strategy, try again");
                            break;
                        }
                        game.SetStratedy(num);
                        break;
                    case 7:
                        // exit
                        Console.WriteLine("Exitted");
                        break;
                    default:
                        Console.WriteLine("No such menu item, try again");
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
