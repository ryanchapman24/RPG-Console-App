using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPRPGtest
{
    class Program
    {
        static void Main(string[] args)
        {
            // To teach the students basic C# interaction with console
            //Console.WriteLine("Hello World!");
            //var consoleTest = Console.ReadLine();
            Game game = new Game();
            game.Start();
        }
    }
}
