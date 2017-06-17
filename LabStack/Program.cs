using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStack
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = Engine.Instance;
            int a = 0;
            while (a != 6)
            {
                Console.WriteLine("Menu");
                Console.WriteLine("0. Create soldiers");
                Console.WriteLine("1. Show armies");
                Console.WriteLine("2. Next turn");
                Console.WriteLine("3. Undo turn");
                Console.WriteLine("4. Redo turn");
                Console.WriteLine("5. Play game until end");
                Console.WriteLine("6. Exit");
                Console.Write("Enter menu item: ");
                while (!int.TryParse(Console.ReadLine(), out a)) ;
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
