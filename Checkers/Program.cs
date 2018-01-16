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

            string playerOne = UI.GetNamesPlayers(1);
            string playerTwo = UI.GetNamesPlayers(2);

            UI.PrintCells(Pole.Cells);

            UI.PrintFigs(Pole.WhiteFigs, Pole.BlackFigs);

            Pole.Game(playerOne, playerTwo);

            Console.ReadKey();
        }
    }
}
