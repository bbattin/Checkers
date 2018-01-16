using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class UI
    {
        /// <summary>
        /// печать белой клетки
        /// </summary>
        /// <param name="OneCell"></param>
        public static void PrintWhite(Cell OneCell)
        {
            PrintOneCell(OneCell, ConsoleColor.Gray);
        }

        /// <summary>
        /// печать черной клетки
        /// </summary>
        /// <param name="OneCell"></param>
        public static void PrintBlack(Cell OneCell)
        {
            PrintOneCell(OneCell, ConsoleColor.DarkGray);
        }

        /// <summary>
        /// печать клетки при выборе фигуры для хода
        /// </summary>
        /// <param name="OneCell"></param>
        public static void PrintSelect(Cell OneCell)
        {
            PrintOneCell(OneCell, ConsoleColor.Cyan);
        }

        /// <summary>
        /// печать клетки с возможным ходом
        /// </summary>
        /// <param name="OneCell"></param>
        public static void PrintMove(Cell OneCell)
        {
            PrintOneCell(OneCell, ConsoleColor.Green);
        }

        /// <summary>
        /// размер клетки экранный
        /// </summary>
        const int SIZECELL = 3;

        /// <summary>
        /// экранная координата начала печати поля по иксу
        /// </summary>
        const int XBEGIN = 5;

        /// <summary>
        /// экранная координата печати поля по игрику
        /// </summary>
        const int YBEGIN = 3;

        /// <summary>
        /// печать одной клетки
        /// </summary>
        /// <param name="OneCell"></param>
        /// <param name="Col"></param>
        public static void PrintOneCell(Cell OneCell, ConsoleColor Col)
        {
            for (int i = 0; i < SIZECELL; i++)
            {
                for (int j = 0; j < SIZECELL; j++)
                {
                    int xout = XBEGIN + OneCell.x * SIZECELL;
                    int yout = YBEGIN + OneCell.y * SIZECELL;
                    Console.BackgroundColor = Col;
                    Console.SetCursorPosition(xout + j, yout + i);
                    Console.Write(" ");
                }
            }
        }

        /// <summary>
        /// печать всех клеток 
        /// </summary>
        public static void PrintCells(Cell[,] cells)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // белые
                    if (cells[i, j].Color == CellColor.White)
                    {
                        PrintWhite(cells[i, j]);
                    }
                    else
                    {
                        PrintBlack(cells[i, j]);
                    }
                }
            }
        }

        /// <summary>
        /// печать одной фигуры
        /// </summary>
        /// <param name="OneFig"></param>
        /// <param name="ColFig"></param>
        /// <param name="back"></param>
        public static void PrintOneFigure(Figure OneFig, ConsoleColor ColFig, ConsoleColor back = ConsoleColor.DarkGray)
        {

            int xout = XBEGIN + OneFig.x * SIZECELL;
            int yout = YBEGIN + OneFig.y * SIZECELL;
            Console.BackgroundColor = back;
            Console.ForegroundColor = ColFig;
            Console.SetCursorPosition(xout + 1, yout + 1);
            Console.Write("O");

        }

        /// <summary>
        /// печать белой фигуры
        /// </summary>
        /// <param name="OneFig"></param>
        public static void PrintWhiteFig(Figure OneFig)
        {
            PrintOneFigure(OneFig, ConsoleColor.White);
        }

        /// <summary>
        /// печать черной фигуры
        /// </summary>
        /// <param name="OneFig"></param>
        public static void PrintBlackFig(Figure OneFig)
        {
            PrintOneFigure(OneFig, ConsoleColor.Black);
        }

        /// <summary>
        /// печать всех фигур
        /// </summary>
        public static void PrintFigs(Figure[] WhiteFigs, Figure[] BlackFigs)
        {
            for (int i = 0; i < BoardLogic.FIGSCOUNT; i++)
            {
                PrintWhiteFig(WhiteFigs[i]);
                PrintBlackFig(BlackFigs[i]);

            }
        }

        /// <summary>
        /// печать выделенной фигуры
        /// </summary>
        /// <param name="select"></param>
        public static void PrintSelecByFig(Figure select, Cell[,] cells)
        {
            Cell sel = cells[select.x, select.y];
            PrintSelect(sel);
        }

        /// <summary>
        /// печать выделенной клетки
        /// </summary>
        /// <param name="select"></param>
        public static void PrintCellByFig(Figure select, Cell[,] cells)
        {
            Cell sel = cells[select.x, select.y];
            PrintOneCell(sel, ConsoleColor.DarkGray);
        }

        public static void PrintNumberPlayer(string player)
        {
            Console.SetCursorPosition(40, 3);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("  Player {0}  ", player);
        }

        public static void PrintWinner(string player)
        {
            Console.Clear();
            Console.SetCursorPosition(30, 10);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0}, you win!", player);
            Console.ReadKey();
            Environment.Exit(0);
        }

        public static string GetNamesPlayers(int numberPlayer)
        {
            Console.SetCursorPosition(5, 3);
            Console.WriteLine("Enter player name {0}: ", numberPlayer);
            Console.SetCursorPosition(5, 5);
            string player = Console.ReadLine();
            Console.Clear();
            return player;

        }
    }
}
