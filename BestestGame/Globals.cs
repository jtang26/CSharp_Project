using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpGame
{
    internal class Globals
    {
        public static bool GameOver { get; set; }
        public static int TimePlayed { get; set; }
        public static int TimesHit { get; set; }
        public static int NumberOfEnemies { get; set; }
        public static int GridHeight { get; set; }
        public static int GridWidth { get; set; }
        public static GameObject[,] Grid { get; set; }
        public static Random Rng { get; set; }
        public static GameObject Player { get; set; }
        public static List<GameObject> EnemyList { get; set; }
        public static bool WritingToConsole { get; set; }
        public static Thread countTimeThread;
        public static Thread moveEnemyThread;
        public static Thread movePlayerThread;
    }
}
