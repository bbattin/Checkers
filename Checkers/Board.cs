using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Board
    {
        /// <summary>
        /// количество фигур одного цвета
        /// </summary>
        const int CnstFigCnt = 12;

        /// <summary>
        /// массив клеток
        /// </summary>
        Cell[,] Cells;

        /// <summary>
        /// массивы черных и белых фигур
        /// </summary>
        Figure[] WhiteFigs = new Figure[CnstFigCnt];
        Figure[] BlackFigs = new Figure[CnstFigCnt];

        /// <summary>
        /// флаг хода белых
        /// </summary>
        bool isWhiteMove;

        // конструктор
        public Board()
        {
            // так как первыми ходят белые поэтому true
            isWhiteMove = true;
            Init();
        }

      
        /// <summary>
        /// заполнение массивов клеток и фигур, привязка их друг к другу 
        /// </summary>
        public void Init()
        {
            int iWhite = 0;
            int iBlack = 0;

            // создание массива
            Cells = new Cell[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Cell OneCell = new Cell();
                    OneCell.x = i;
                    OneCell.y = j;
                   
                    // черные
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1))
                    {
                        OneCell.Color = CellColor.White;
                    }
                    else
                    {               
                        OneCell.Color = CellColor.Black;
                        // расставляем фигуры - черные вверху
                        if (j < 3)
                        {
                            Figure NewFig = new Figure();
                            NewFig.x = i;
                            NewFig.y = j;
                            NewFig.State = FigureState.Black;

                            // устанавливаем связь фигуры с клеткой
                            OneCell.Fig = NewFig;
                            BlackFigs[iBlack] = NewFig;
                            iBlack++;
                            
                        }

                        // тут белые
                        if (j > 4)
                        {
                            Figure NewFig = new Figure();
                            NewFig.x = i;
                            NewFig.y = j;
                            NewFig.State = FigureState.White;

                            // устанавливаем связь фигуры с клекой
                            OneCell.Fig = NewFig;
                            WhiteFigs[iWhite] = NewFig;
                            iWhite++;

                        }

                    }
                    Cells[i, j] = OneCell;

                }
            }
        }


        /// <summary>
        /// печать белой клетки
        /// </summary>
        /// <param name="OneCell"></param>
        public void PrintWhite(Cell OneCell)
        {
            PrintOneCell(OneCell, ConsoleColor.Gray);
        }

        /// <summary>
        /// печать черной клетки
        /// </summary>
        /// <param name="OneCell"></param>
        public void PrintBlack(Cell OneCell)
        {
            PrintOneCell(OneCell, ConsoleColor.DarkGray);
        }

        /// <summary>
        /// печать клетки при выборе фигуры для хода
        /// </summary>
        /// <param name="OneCell"></param>
        public void PrintSelect(Cell OneCell)
        {
            PrintOneCell(OneCell, ConsoleColor.Cyan);
        }

        /// <summary>
        /// печать клетки с возможным ходом
        /// </summary>
        /// <param name="OneCell"></param>
        public void PrintMove(Cell OneCell)
        {
            PrintOneCell(OneCell, ConsoleColor.Green);
        }

        /// <summary>
        /// размер клетки экранный
        /// </summary>
        const int sizeCell = 3;

        /// <summary>
        /// экранная координата начала печати поля по иксу
        /// </summary>
        const int xConst = 5;

        /// <summary>
        /// экранная координата печати поля по игрику
        /// </summary>
        const int yConst = 3;

        /// <summary>
        /// печать одной клетки
        /// </summary>
        /// <param name="OneCell"></param>
        /// <param name="Col"></param>
        public void PrintOneCell(Cell OneCell, ConsoleColor Col)
        {
            for (int i = 0; i < sizeCell; i++)
            {
                for (int j = 0; j < sizeCell; j++)
                {
                    int xout = xConst + OneCell.x * sizeCell;
                    int yout = yConst + OneCell.y * sizeCell;
                    Console.BackgroundColor = Col;
                    Console.SetCursorPosition(xout + j, yout + i);
                    Console.Write(" ");
                }
            }
        }

        /// <summary>
        /// печать всех клеток 
        /// </summary>
        public void PrintCells()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // белые
                    if (Cells[i, j].Color == CellColor.White)
                    {
                        PrintWhite(Cells[i, j]);
                    }
                    else
                    {
                        PrintBlack(Cells[i, j]);
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
        public void PrintOneFigure(Figure OneFig, ConsoleColor ColFig, ConsoleColor back = ConsoleColor.DarkGray)
        {

            int xout = xConst + OneFig.x * sizeCell;
            int yout = yConst + OneFig.y * sizeCell;
            Console.BackgroundColor = back;
            Console.ForegroundColor = ColFig;
            Console.SetCursorPosition(xout + 1, yout + 1);
            Console.Write("O");

        }

        /// <summary>
        /// печать белой фигуры
        /// </summary>
        /// <param name="OneFig"></param>
        public void PrintWhiteFig(Figure OneFig)
        {
            PrintOneFigure(OneFig, ConsoleColor.White);
        }

        /// <summary>
        /// печать черной фигуры
        /// </summary>
        /// <param name="OneFig"></param>
        public void PrintBlackFig(Figure OneFig)
        {
            PrintOneFigure(OneFig, ConsoleColor.Black);
        }

        /// <summary>
        /// печать всех фигур
        /// </summary>
        public void PrintFigs()
        {
            for (int i = 0; i < CnstFigCnt; i++)
            {
                PrintWhiteFig(WhiteFigs[i]);
                PrintBlackFig(BlackFigs[i]);

            }
        }
        /// <summary>
        /// проверка на правильность координат, чтобы не уходили за поле
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>bool</returns>
        public bool CheckBorder(int x, int y)
        {
            return x >= 0 && y >= 0 && x < 8 && y < 8;
        }

        /// <summary>
        /// получаем структуру с возможностью хода и возможными координатами
        /// </summary>
        /// <param name="Fig"></param>
        /// <returns></returns>
        public Figure GetFigMove(Figure Fig)
        {
            FigMove theMove = new FigMove();

            theMove.isMove = false;
            theMove.isFight = false;

            Coordinate left, right;

            if (isWhiteMove)
            {
                left  = new Coordinate(Fig.x - 1, Fig.y - 1);
                right = new Coordinate(Fig.x + 1, Fig.y - 1);
            }
            else
            {
                left = new Coordinate(Fig.x - 1, Fig.y + 1);
                right = new Coordinate(Fig.x + 1, Fig.y + 1);
            }

            // проверка хода налево
            if (CheckBorder(left.x, left.y))
            {
                // простой ход - клетка свободна
                if (Cells[left.x, left.y].Fig == null)
                {
                    theMove.isMove = true;
                    theMove.left = left;
                }
                // клетка занята, нужно проверить возможность боя
                else 
                {
                    Figure fig = Cells[left.x, left.y].Fig;
                    // если на клетке стоит черная фигура и ход белых
                    if (fig.State == FigureState.Black && isWhiteMove)
                    {
                        // проверка клетки куда прыгаем после боя
                        Coordinate aftreFight = new Coordinate(fig.x - 1, fig.y - 1);
                        // если эта клетка на доске и свободна то можно бить
                        if (CheckBorder(aftreFight.x, aftreFight.y) && Cells[aftreFight.x, aftreFight.y].Fig == null)
                        {
                            theMove.isFight = true;
                            theMove.left = aftreFight;
                            // клетка которую перепрыгиваем и на котрой стоит шашка другого цвета
                            theMove.fight = left;
                        }
                    }
                    // если на клетке стоит белая фигура и ход черных
                    if (fig.State == FigureState.White && isWhiteMove == false)
                    {
                        // проверка клетки куда прыгаем после боя
                        Coordinate aftreFight = new Coordinate(fig.x - 1, fig.y + 1);
                        // если эта клетка на доске и свободна то можно бить
                        if (CheckBorder(aftreFight.x, aftreFight.y) && Cells[aftreFight.x, aftreFight.y].Fig == null)
                        {
                            theMove.isFight = true;
                            theMove.left = aftreFight;
                            theMove.fight = left;
                        }
                    }
                }
            }

            // проверка хода вправо
            if (CheckBorder(right.x, right.y))
            { 
                if (Cells[right.x, right.y].Fig == null)
                {
                    // тольк если ход на лево не был боем, оставим возможность походить вправо
                    if (theMove.isFight == false)
                    {
                        theMove.isMove = true;
                        theMove.right = right;
                    }
                }
                else
                {
                    Figure fig = Cells[right.x, right.y].Fig;
                    // если на клетке стоит черная фигура и ход белых
                    if (fig.State == FigureState.Black && isWhiteMove)
                    {
                        // проверка клетки куда прыгаем после боя
                        Coordinate aftreFight = new Coordinate(fig.x + 1, fig.y - 1);
                        // если эта клетка на доске и свободна то можно бить
                        if (CheckBorder(aftreFight.x, aftreFight.y) && Cells[aftreFight.x, aftreFight.y].Fig == null)
                        {
                            theMove.isFight = true;
                            theMove.right = aftreFight;
                            theMove.fight = right;

                            // если определили что можно походить на лево, то это нужно отменить при бое
                            if (theMove.isMove)
                            {
                                theMove.isMove = false;
                                theMove.left = null;
                            }
                        }
                    }
                    // если на клетке стоит белая фигура и ход черных
                    if (fig.State == FigureState.White && isWhiteMove == false)
                    {
                        // проверка клетки куда прыгаем после боя
                        Coordinate aftreFight = new Coordinate(fig.x + 1, fig.y + 1);
                        // если эта клетка на доске и свободна то можно бить
                        if (CheckBorder(aftreFight.x, aftreFight.y) && Cells[aftreFight.x, aftreFight.y].Fig == null)
                        {
                            theMove.isFight = true;
                            theMove.right = aftreFight;
                            theMove.fight = right;
                            // если определили что можно походить на лево, то это нужно отменить при бое
                            if (theMove.isMove)
                            {
                                theMove.isMove = false;
                                theMove.left = null;
                            }
                        }
                    }

                }
            }

            Fig.move = theMove;
            return Fig;
        }

        /// <summary>
        /// получаем массив с белыми фигурами, которые могут походить на пустую клетку или массив фигур для боя
        /// </summary>
        /// <returns></returns>
        public Figure[] GetWhiteMoves()
        {
            // массив фигур с возможным ходом
            Figure[] figuresToMove = new Figure[CnstFigCnt];
            int figMoveCnt = 0;

            // массив фигур с возможным боем
            Figure[] figuresToFight = new Figure[CnstFigCnt];
            int figFightCnt = 0;

            for (int i = 0; i < CnstFigCnt; i++)
            {
                // если фигура удалена из списка (побита) то пропускаем ее
                if (WhiteFigs[i] == null) continue;

                Figure theFig = GetFigMove(WhiteFigs[i]);
                if (theFig.move.isMove)
                {
                    figuresToMove[figMoveCnt] = theFig;
                    figMoveCnt++;
                }

                if (theFig.move.isFight)
                {
                    figuresToFight[figFightCnt] = theFig;
                    figFightCnt++;
                }
            }

            Figure[] returnToMove;

            // фигуры для боя имеют приимущество
            if (figFightCnt > 0)
            {
                returnToMove = new Figure[figFightCnt];
                for (int i = 0, j = 0; i < CnstFigCnt; i++)
                {
                    if (figuresToFight[i] != null)
                    {
                        returnToMove[j] = figuresToFight[i];
                        j++;
                    }
                }
            }
            else
            {
                returnToMove = new Figure[figMoveCnt];
                for (int i = 0, j = 0; i < CnstFigCnt; i++)
                {
                    if (figuresToMove[i] != null)
                    {
                        returnToMove[j] = figuresToMove[i];
                        j++;
                    }
                }
            }

            return returnToMove;
        }

        /// <summary>
        /// получаем массив с черными фигурами, которые могут походить на пустую клетку или массив фигур для боя
        /// </summary>
        /// <returns></returns>
        public Figure[] GetBlacksMoves()
        {
            // массив фигур с возможным ходом
            Figure[] figuresToMove = new Figure[CnstFigCnt];
            int figMoveCnt = 0;

            // массив фигур с возможным боем
            Figure[] figuresToFight = new Figure[CnstFigCnt];
            int figFightCnt = 0;

            for (int i = 0; i < CnstFigCnt; i++)
            {
                // если фигура удалена из списка (побита) то пропускаем ее
                if (BlackFigs[i] == null) continue;

                Figure theFig = GetFigMove(BlackFigs[i]);
                if (theFig.move.isMove)
                {
                    figuresToMove[figMoveCnt] = theFig;
                    figMoveCnt++;
                }

                if (theFig.move.isFight)
                {
                    figuresToFight[figFightCnt] = theFig;
                    figFightCnt++;
                }
            }

            Figure[] returnToMove;

            // фигуры для боя имеют приимущество
            if (figFightCnt > 0)
            {
                returnToMove = new Figure[figFightCnt];
                for (int i = 0, j = 0; i < CnstFigCnt; i++)
                {
                    if (figuresToFight[i] != null)
                    {
                        returnToMove[j] = figuresToFight[i];
                        j++;
                    }
                }
            }
            else
            {
                returnToMove = new Figure[figMoveCnt];
                for (int i = 0, j = 0; i < CnstFigCnt; i++)
                {
                    if (figuresToMove[i] != null)
                    {
                        returnToMove[j] = figuresToMove[i];
                        j++;
                    }
                }
            }

            return returnToMove;
        }

        /// <summary>
        /// удаляет фигуру из списка доступных фигур, после того как ее побили
        /// </summary>
        public void DelFigure(Figure delFigure)
        {
            // для черных и белых отдельные списки
            if (delFigure.State == FigureState.Black)
            {
                for (int i = 0; i < CnstFigCnt; i++)
                {
                    // если фигура удалена из списка (побита) то пропускаем ее
                    if (BlackFigs[i] == null) continue;

                    // сверяем координаты, если они сопадут значит это искомая фигура и ее надо выкинуть
                    if (BlackFigs[i].x == delFigure.x && BlackFigs[i].y == delFigure.y)
                    {
                        // удалили и сразу выходим из цикла
                        BlackFigs[i] = null;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < CnstFigCnt; i++)
                {
                    // если фигура удалена из списка (побита) то пропускаем ее
                    if (WhiteFigs[i] == null) continue;

                    // сверяем координаты, если они сопадут значит это искомая фигура и ее надо выкинуть
                    if (WhiteFigs[i].x == delFigure.x && WhiteFigs[i].y == delFigure.y)
                    {
                        // удалили и сразу выходим из цикла
                        WhiteFigs[i] = null;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// выбор фигуры для хода
        /// </summary>
        /// <param name="figuresToMove"></param>
        /// <returns></returns>
        public Figure SelectFigureForMove(Figure[] figuresToMove)
        {
            int i = 0;
            Figure select = figuresToMove[i];
            PrintSelecByFig(select);
            PrintOneFigure(select, select.GetColorByState(), ConsoleColor.Cyan);
            ConsoleKey use;

            do
            {
                use = Console.ReadKey(true).Key;
                switch (use)
                {
                    case ConsoleKey.Enter:
                        select = figuresToMove[i];
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.RightArrow:
                        if (i < figuresToMove.Length-1)
                        {
                            i++;
                            PrintCellByFig(select);
                            PrintOneFigure(select, select.GetColorByState());
                            select = figuresToMove[i];
                            PrintSelecByFig(select);
                            PrintOneFigure(select, select.GetColorByState(), ConsoleColor.Cyan);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:
                        if (i > 0)
                        {
                            i--;
                            PrintCellByFig(select);
                            PrintOneFigure(select, select.GetColorByState());
                            select = figuresToMove[i];
                            PrintSelecByFig(select);
                            PrintOneFigure(select, select.GetColorByState(), ConsoleColor.Cyan);
                        }
                        break;

                }

            } while (use != ConsoleKey.Enter && use != ConsoleKey.Escape);

            return select;
        }

        /// <summary>
        /// печать выделенной фигуры
        /// </summary>
        /// <param name="select"></param>
        private void PrintSelecByFig(Figure select)
        {
            Cell sel = Cells[select.x, select.y];
            PrintSelect(sel);
        }

        /// <summary>
        /// печать выделенной клетки
        /// </summary>
        /// <param name="select"></param>
        private void PrintCellByFig(Figure select)
        {
            Cell sel = Cells[select.x, select.y];
            PrintOneCell(sel, ConsoleColor.DarkGray);
        }

        /// <summary>
        /// ход фигурой после ее выбора, изменения состояния прежней и следующей клетки
        /// </summary>
        /// <param name="figure"></param>
        public void MoveFigure(Figure figure)
        {
            
            Cell selectMove = CheckCellsForLeftMove(figure);
            // сюда будем запоминать координату предыдущего выбора хода, если она не меняется то перекрашивать ячейки не будем
            int prevX = selectMove.x;

            PrintMove(selectMove);

            ConsoleKey use;

            do
            {
                use = Console.ReadKey(true).Key;
                switch (use)
                {
                    case ConsoleKey.Enter:

                        if (figure.move.isFight)
                        {
                            // уберем шашку которую перепрыгиваем
                            PrintBlack(Cells[figure.move.fight.x, figure.move.fight.y]);
                            // очистка убитой шашки
                            Figure delFigure = Cells[figure.move.fight.x, figure.move.fight.y].Fig;
                            // удаляем из списка фигур
                            DelFigure(delFigure);
                            // удаляем с поля
                            Cells[figure.move.fight.x, figure.move.fight.y].Fig = null;
                        }

                        // на месте откуда походили рисуем пустую черную клетку (без шашки)
                        PrintBlack(Cells[figure.x, figure.y]);
                        // меняем фон выбора, на фон черной клетки
                        PrintBlack(selectMove);

                        // перемещение фигуры внутри массива Cells - очистка старой
                        Cells[figure.x, figure.y].Fig = null;

                        figure.x = selectMove.x;
                        figure.y = selectMove.y;
                        PrintOneFigure(figure, figure.GetColorByState());

                        // перемещение фигуры внутри массива Cells - установка новой
                        Cells[figure.x, figure.y].Fig = figure;

                        break;

                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.RightArrow:

                        selectMove = CheckCellsForRightMove(figure);
                        if (prevX != selectMove.x)
                        {
                            PrintMove(selectMove);
                            PrintBlack(Cells[figure.move.left.x, figure.move.left.y]);
                            prevX = selectMove.x;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:

                        selectMove = CheckCellsForLeftMove(figure);
                        if (prevX != selectMove.x)
                        {
                            PrintMove(selectMove);
                            PrintBlack(Cells[figure.move.right.x, figure.move.right.y]);
                            prevX = selectMove.x;
                        }
                        break;

                }

            } while (use != ConsoleKey.Enter && use != ConsoleKey.Escape);
            return;
        }

        /// <summary>
        /// проверка на нахождение возможных клеток для хода фигуры в пределах поля и получения координат первой возможной
        /// </summary>
        /// <param name="figure"></param>
        /// <returns></returns>
        private Cell CheckCellsForLeftMove(Figure figure)
        {
            Cell selectMove;

            if (figure.move.left != null)
            {
                selectMove = Cells[figure.move.left.x, figure.move.left.y];

            }
            else
            {
                selectMove = Cells[figure.move.right.x, figure.move.right.y];
            }

            return selectMove;
        }

        private Cell CheckCellsForRightMove(Figure figure)
        {
            Cell selectMove;

            if (figure.move.right != null)
            {
                selectMove = Cells[figure.move.right.x, figure.move.right.y];

            }
            else
            {
                selectMove = Cells[figure.move.left.x, figure.move.left.y];
            }

            return selectMove;
        }

        /// <summary>
        /// метод ходов игры. чередуют ходы белых и черных
        /// </summary>
        public void Game()
        {
            // ходы в цикле
            while (true)
            {
                if (isWhiteMove)
                {
                    Console.SetCursorPosition(40, 3);
                    Console.WriteLine("Player 1");

                    Figure[] figuresToMove = GetWhiteMoves();

                    Figure one = SelectFigureForMove(figuresToMove);

                    MoveFigure(one);

                    isWhiteMove = false;
                    
                }
                else
                {
                    Console.SetCursorPosition(40, 3);
                    Console.WriteLine("Player 2");

                    Figure[] figuresToMove = GetBlacksMoves();

                    Figure one = SelectFigureForMove(figuresToMove);

                    MoveFigure(one);

                    isWhiteMove = true;
                }
            }
        }

    }
}