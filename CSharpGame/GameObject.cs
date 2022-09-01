using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpGame
{
    internal class GameObject
    {
        public ConsoleColor? BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public string Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw()
        {
            Console.BackgroundColor = BackgroundColor ?? Globals.Grid[(int)X,(int)Y].BackgroundColor ?? ConsoleColor.Black;
            Console.ForegroundColor = ForegroundColor;
            Console.Write($"{Symbol} ");
        }
        public bool Move(int x,int y)
        {
            while(Globals.WritingToConsole)
            {
                Thread.Sleep(100);
            }
            Globals.WritingToConsole = true;
            var moveSuccess = true;
            Console.SetCursorPosition(X*2,Y);
            Globals.Grid[X,Y].Draw();
            if (x>=0 && x < Globals.Grid.GetLength(0))
                    X= x;
                else
                    moveSuccess = false;
                if(y>=0 && y < Globals.Grid.GetLength(1))
                    Y= y;
                else
                    moveSuccess = false;
                Console.SetCursorPosition(X*2,Y);
                Draw();
                Console.SetCursorPosition(26,26);
            Globals.WritingToConsole = false;
            return moveSuccess;
        }
        public bool Collides(GameObject obj)
        {
            if ((int)X == (int)obj.X && (int)Y == (int)obj.Y)
                return true;
            return false;
        }
    }
}
