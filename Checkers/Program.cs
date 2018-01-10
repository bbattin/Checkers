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
                                  
            Board Pole = new Board();
            Pole.PrintCells();
                      
            Pole.PrintFigs();

            Figure[] figuresToMove = Pole.GetBlacksMoves();

            Figure one = Pole.SelectFigureForMove(figuresToMove);

            Pole.MoveFigure(one);
               


            Console.ReadKey();
        }
    }
}
