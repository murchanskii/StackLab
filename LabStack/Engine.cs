using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using LabStack.ArmyFolder;
using LabStack.Commands;
using LabStack.Strategies;

namespace LabStack
{
    class Engine
    {
        private Army _army1;
        private Army _army2;
        private readonly Random _rnd;

        private Stack<ICommand> _undoCommands;
        private Stack<ICommand> _redoCommands;

        private Strategy _currentStrategy;

        private static Engine _instance;
        public static Engine Instance => _instance ?? (_instance = new Engine());

        private Engine()
        {
            _rnd = new Random();
        }

        private void InitializeFile(string path)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.Default);
            sw.Dispose();
            sw.Close();
        }

        public void CreateArmy()
        {
            _isDraw = false;
            _winnerExists = false;
            _undoCommands = new Stack<ICommand>();
            _army1 = new Army("Army Sun", 200);
            Thread.Sleep(1000);
            _army2 = new Army("Army Moon", 200);
            InitializeFile(@"deaths.txt");
            InitializeFile(@"healer_log.txt");
            SetStratedy(0); // default strategy OneColumn()

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Armies created");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void SetStratedy(int numOfStrategy)
        {
            if (_army1 == null || _army2 == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CREATE ARMIES");
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }
            Strategy strategy = new OneColumn(_army1, _army2, _undoCommands, _rnd);
            switch (numOfStrategy)
            {
                case 1:
                    strategy = new NColumns(_army1, _army2, _undoCommands, _rnd);
                    break;
                case 2:
                    strategy = new AllVsAll(_army1, _army2, _undoCommands, _rnd);
                    break;
            }
            _currentStrategy = strategy;
            Console.WriteLine("|===============================================" +
                              "============================================|\n");
            ShowArmies();
            Console.WriteLine("|===============================================" +
                              "============================================|");
        }

        public void ShowArmies()
        {
            if (_army1 != null)
                _currentStrategy.ShowArmy(_army1);
            if (_army2 != null)
                _currentStrategy.ShowArmy(_army2);
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You must create soldiers");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private bool _isDraw;
        private bool _winnerExists;

        public void DoTurnUntilEnd()
        {
            if (_army1 == null || _army2 == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CREATE ARMIES");
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }
            while (_army1.soldiers.Count != 0 && _army2.soldiers.Count != 0)
            {
                Turn();
                if (_isDraw || _winnerExists)
                    break;
                ShowArmies();
                Console.WriteLine("|===========================================" +
                                  "================================================|");
            }

        }

        private void Draw()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("It's a DRAW");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void CheckForWinner()
        {
            if (_army1.soldiers.Count == 0)
            {
                WinnerMessage(_army2);
                _winnerExists = true;
            }
            if (_army2.soldiers.Count == 0)
            {
                WinnerMessage(_army1);
                _winnerExists = true;
            }
        }

        private void WinnerMessage(Army winner)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"({winner.Name}) is WINNER");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Turn()
        {
            _redoCommands = new Stack<ICommand>();
            _currentStrategy.Turn();

            int turnsWithoutDeath = 15;
            if (_undoCommands.Count >= turnsWithoutDeath)
            {
                if (!_undoCommands.ToList()
                    .GetRange(_undoCommands.Count - turnsWithoutDeath, turnsWithoutDeath)
                    .Select(command => command is DeathCommand)
                    .Any())
                {
                    _isDraw = true;
                    Draw();
                }
            }
            CheckForWinner();
        }

        public void Undo()
        {
            if (_undoCommands == null || _undoCommands.Count == 0)
            {
                Console.WriteLine("No Undo");
                return;
            }
            var cmd = _undoCommands.Pop();
            cmd.Undo();
            _redoCommands.Push(cmd);
            if (cmd is DeathCommand)
            {
                cmd = _undoCommands.Pop();
                cmd.Undo();
                _redoCommands.Push(cmd);
            }
            
        }

        public void Redo()
        {
            if (_redoCommands == null || _redoCommands.Count == 0)
            {
                Console.WriteLine("No Redo");
                return;
            }
            var cmd = _redoCommands.Pop();
            cmd.Do();
            _undoCommands.Push(cmd);
            if (_redoCommands.Count != 0 && _redoCommands.Peek() is DeathCommand)
            {
                cmd = _redoCommands.Pop();
                cmd.Do();
                _undoCommands.Push(cmd);
            }

        }
    }
}
