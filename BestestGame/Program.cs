using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the enemy count:");
            Globals.NumberOfEnemies = int.Parse(Console.ReadLine());
            Console.Clear();
            var engine = new Engine();
            engine.InitializeGame();
            for (;;)
            {
                if(Globals.GameOver)
                {
                    Globals.moveEnemyThread.Abort();
                    Globals.movePlayerThread.Abort();
                    Globals.countTimeThread.Abort();
                    Console.Clear();
                    Console.WriteLine("GAME OVER!");
                    Thread.Sleep(3000);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    engine.InitializeGame();
                }
            }
        }
    }
}
