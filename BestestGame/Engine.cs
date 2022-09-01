using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpGame
{
    internal class Engine
    {
        public void InitializeGame()
        {
            Globals.GameOver = false;
            Globals.Rng = new Random();
            Globals.GridHeight = 25;
            Globals.GridWidth = 25;
            Globals.Grid = new GameObject[Globals.GridWidth,Globals.GridHeight];
            Globals.Player = new GameObject
            {
                BackgroundColor = ConsoleColor.Blue,
                ForegroundColor = ConsoleColor.Magenta,
                Symbol = "■",
                Y = Globals.GridHeight/2,
                X = Globals.GridWidth/2,
            };
           Globals.EnemyList = new List<GameObject>();
            for (var x = 0;x < Globals.NumberOfEnemies;x++)
                Globals.EnemyList.Add(new GameObject
                {
                    BackgroundColor = null,
                    ForegroundColor = ConsoleColor.DarkMagenta,
                    Symbol = "■",
                    Y = Globals.Rng.Next(Globals.Grid.GetLength(0)),
                    X = Globals.Rng.Next(Globals.Grid.GetLength(1)),
                });
            for (int y = 0;y < Globals.Grid.GetLength(1);y++)
                for (int x = 0;x < Globals.Grid.GetLength(0);x++)
                    Globals.Grid[x,y] = new GameObject
                    {
                        ForegroundColor = Globals.Rng.NextDouble() < 0.5 ? ConsoleColor.Green : ConsoleColor.DarkGreen,
                        BackgroundColor = Globals.Rng.NextDouble() < 0.5 ? ConsoleColor.DarkGray : ConsoleColor.Gray,
                        Symbol = Globals.Rng.NextDouble() < 0.5 ? "W" : "w"
                    };
            DrawGrid();
            SetupThreads();
        }
        private void SetupThreads()
        {
            Globals.countTimeThread = new Thread(CountTime);
            Globals.moveEnemyThread = new Thread(MoveEnemy);
            Globals.movePlayerThread = new Thread(MovePlayerThread);
            Globals.countTimeThread.Start();
            Globals.moveEnemyThread.Start();
            Globals.movePlayerThread.Start();
        }
        public void DrawGrid()
        {
            for (int y = 0;y < Globals.Grid.GetLength(1);y++)
            { 
                for (int x = 0;x < Globals.Grid.GetLength(0);x++)
                    { 
                    var placedEnemy = false;
                    foreach (var enemy in Globals.EnemyList)
                        if (x == enemy.X && y == enemy.Y)
                        {
                            enemy.Draw();
                            placedEnemy = true;
                            break;
                        }
                    if (!placedEnemy)
                    { 
                        if (x == Globals.Player.X && y == Globals.Player.Y)
                            Globals.Player.Draw();
                        else
                            Globals.Grid[x,y].Draw();
                    }
                }
                Console.WriteLine();
            }
        }
        public void MovePlayer(ConsoleKeyInfo keyInfo)
        {
            
                switch (keyInfo.Key)
                {
                case ConsoleKey.W:Globals.Player.Move(Globals.Player.X,Globals.Player.Y-1);break;
                case ConsoleKey.S:Globals.Player.Move(Globals.Player.X,Globals.Player.Y+1);break;
                case ConsoleKey.A:Globals.Player.Move(Globals.Player.X-1,Globals.Player.Y);break;
                case ConsoleKey.D:Globals.Player.Move(Globals.Player.X+1,Globals.Player.Y);break;
                }
        }
        public void MoveEnemy()
        {
            for (;;)
            {
                Thread.Sleep(100);
                foreach (var enemy in Globals.EnemyList) 
                {
                if(Math.Abs(Globals.Player.X-enemy.X)<=3 && Math.Abs(Globals.Player.Y-enemy.Y)<=3)
                {
                    if(Globals.Player.X>enemy.X)
                    {
                        enemy.Move(enemy.X+1,enemy.Y);
                    }
                    else if(Globals.Player.X<enemy.X)
                    {
                        enemy.Move(enemy.X-1,enemy.Y);
                    }
                    else if(Globals.Player.Y>enemy.Y)
                    {
                        enemy.Move(enemy.X,enemy.Y+1);
                    }
                    else if (Globals.Player.Y < enemy.Y)
                    {
                        enemy.Move(enemy.X,enemy.Y-1);
                    }
                    else
                    {
                        enemy.Move(enemy.X,enemy.Y);
                    }
                }
                    if (Globals.Player.Collides(enemy))
                    { 
                        Globals.TimesHit++;
                        new Thread(ShowScoreAndTime).Start();
                        Globals.Player.Move(Globals.Player.X,Globals.Player.Y);
                        Globals.GameOver = true;

                    }
                }
            }
        }
        public void CountTime()
        {
            for (;;)
            {
                Globals.TimePlayed++;
                new Thread(ShowScoreAndTime).Start();
                Thread.Sleep(1000);
            }
        }
        public void ShowScoreAndTime()
        {
            while (Globals.WritingToConsole)
            {
            }
            Globals.WritingToConsole = true;
            Console.SetCursorPosition(52,0);
            Console.WriteLine($"Time Played: {TimeSpan.FromSeconds(Globals.TimePlayed).ToString(@"hh\:mm\:ss")}");
            Console.SetCursorPosition(52,1);
            Console.WriteLine($"Time Hit: {Globals.TimesHit}");
            Globals.WritingToConsole = false;
        }
        public void MovePlayerThread()
        {
            for (;;)
            {
                MovePlayer(Console.ReadKey(true));
            }
        }
    }
}
