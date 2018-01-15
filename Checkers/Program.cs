using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
                                  
            BoardLogic Pole = new BoardLogic();
            Pole.PrintCells();

            Pole.PrintFigs();

            Pole.Game();

            Console.ReadKey();
        }
    }
}
