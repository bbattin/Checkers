using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class BoardLogic
    {
        /// <summary>
        /// количество фигур одного цвета
        /// </summary>
        public const int FIGSCOUNT = 12;

        /// <summary>
        /// массив клеток
        /// </summary>
        public Cell[,] Cells;

        /// <summary>
        /// массивы черных и белых фигур
        /// </summary>
        public Figure[] WhiteFigs = new Figure[FIGSCOUNT];
        public Figure[] BlackFigs = new Figure[FIGSCOUNT];

        /// <summary>
        /// флаг хода белых
        /// </summary>
        bool isWhiteMove;

        // конструктор
        public BoardLogic()
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
                    theMove.left = new MoveFight(left);
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
                            // ход с боем
                            theMove.left = new MoveFight(aftreFight, left);
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
                            // ход с боем
                            theMove.left = new MoveFight(aftreFight, left);
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
                        theMove.right = new MoveFight(right);
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
                            theMove.right = new MoveFight(aftreFight, right);

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
                            theMove.right = new MoveFight(aftreFight, right);
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
            
            // бой назад
            Coordinate backleft, backright;

            if (isWhiteMove)
            {
                left = new Coordinate(Fig.x - 1, Fig.y + 1);
                right = new Coordinate(Fig.x + 1, Fig.y + 1);
            }
            else
            {
                left = new Coordinate(Fig.x - 1, Fig.y - 1);
                right = new Coordinate(Fig.x + 1, Fig.y - 1);
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
            Figure[] figuresToMove = new Figure[FIGSCOUNT];
            int figMoveCnt = 0;

            // массив фигур с возможным боем
            Figure[] figuresToFight = new Figure[FIGSCOUNT];
            int figFightCnt = 0;

            for (int i = 0; i < FIGSCOUNT; i++)
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

            Figure[] returnToMove = null;

            // фигуры для боя имеют приимущество
            if (figFightCnt > 0)
            {
                returnToMove = new Figure[figFightCnt];
                for (int i = 0, j = 0; i < FIGSCOUNT; i++)
                {
                    if (figuresToFight[i] != null)
                    {
                        returnToMove[j] = figuresToFight[i];
                        j++;
                    }
                }
            }
            // если есть ход и нет боя
            if (figFightCnt ==0 && figMoveCnt > 0)
            {
                returnToMove = new Figure[figMoveCnt];
                for (int i = 0, j = 0; i < FIGSCOUNT; i++)
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
            Figure[] figuresToMove = new Figure[FIGSCOUNT];
            int figMoveCnt = 0;

            // массив фигур с возможным боем
            Figure[] figuresToFight = new Figure[FIGSCOUNT];
            int figFightCnt = 0;

            for (int i = 0; i < FIGSCOUNT; i++)
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

            Figure[] returnToMove = null;

            // фигуры для боя имеют приимущество
            if (figFightCnt > 0)
            {
                returnToMove = new Figure[figFightCnt];
                for (int i = 0, j = 0; i < FIGSCOUNT; i++)
                {
                    if (figuresToFight[i] != null)
                    {
                        returnToMove[j] = figuresToFight[i];
                        j++;
                    }
                }
            }
            // если есть ход и нет боя
            if (figFightCnt == 0 && figMoveCnt > 0)
            {
                returnToMove = new Figure[figMoveCnt];
                for (int i = 0, j = 0; i < FIGSCOUNT; i++)
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
                for (int i = 0; i < FIGSCOUNT; i++)
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
                for (int i = 0; i < FIGSCOUNT; i++)
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
            UI.PrintSelecByFig(select, Cells);
            UI.PrintOneFigure(select, select.GetColorByState(), ConsoleColor.Cyan);
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
                            UI.PrintCellByFig(select, Cells);
                            UI.PrintOneFigure(select, select.GetColorByState());
                            select = figuresToMove[i];
                            UI.PrintSelecByFig(select, Cells);
                            UI.PrintOneFigure(select, select.GetColorByState(), ConsoleColor.Cyan);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:
                        if (i > 0)
                        {
                            i--;
                            UI.PrintCellByFig(select, Cells);
                            UI.PrintOneFigure(select, select.GetColorByState());
                            select = figuresToMove[i];
                            UI.PrintSelecByFig(select, Cells);
                            UI.PrintOneFigure(select, select.GetColorByState(), ConsoleColor.Cyan);
                        }
                        break;

                }

            } while (use != ConsoleKey.Enter && use != ConsoleKey.Escape);

            return select;
        }

        

        /// <summary>
        /// ход фигурой после ее выбора, изменения состояния прежней и следующей клетки
        /// </summary>
        /// <param name="figure"></param>
        public void MoveFigure(Figure figure)
        {

            MoveFight selectMove = CheckCellsForLeftMove(figure);
            // сюда будем запоминать координату предыдущего выбора хода, если она не меняется то перекрашивать ячейки не будем
            int prevX = selectMove.move.x;

            UI.PrintMove(Cells[selectMove.move.x, selectMove.move.y]);

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
                            UI.PrintBlack(Cells[selectMove.fight.x, selectMove.fight.y]);
                            // очистка убитой шашки
                            Figure delFigure = Cells[selectMove.fight.x, selectMove.fight.y].Fig;
                            // удаляем из списка фигур
                            DelFigure(delFigure);
                            // удаляем с поля
                            Cells[selectMove.fight.x, selectMove.fight.y].Fig = null;
                        }

                        // на месте откуда походили рисуем пустую черную клетку (без шашки)
                        UI.PrintBlack(Cells[figure.x, figure.y]);
                        // меняем фон выбора, на фон черной клетки
                        UI.PrintBlack(Cells[selectMove.move.x, selectMove.move.y]);

                        // перемещение фигуры внутри массива Cells - очистка старой
                        Cells[figure.x, figure.y].Fig = null;

                        figure.x = selectMove.move.x;
                        figure.y = selectMove.move.y;
                        UI.PrintOneFigure(figure, figure.GetColorByState());

                        // перемещение фигуры внутри массива Cells - установка новой
                        Cells[figure.x, figure.y].Fig = figure;

                        break;

                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.RightArrow:

                        selectMove = CheckCellsForRightMove(figure);
                        if (prevX != selectMove.move.x)
                        {
                            UI.PrintMove(Cells[selectMove.move.x, selectMove.move.y]);
                            UI.PrintBlack(Cells[figure.move.left.move.x, figure.move.left.move.y]);
                            prevX = selectMove.move.x;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:

                        selectMove = CheckCellsForLeftMove(figure);
                        if (prevX != selectMove.move.x)
                        {
                            UI.PrintMove(Cells[selectMove.move.x, selectMove.move.y]);
                            UI.PrintBlack(Cells[figure.move.right.move.x, figure.move.right.move.y]);
                            prevX = selectMove.move.x;
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
        private MoveFight CheckCellsForLeftMove(Figure figure)
        {
            MoveFight selectMove;

            if (figure.move.left != null)
            {
                selectMove = figure.move.left;
            }
            else
            {
                selectMove = figure.move.right;
            }

            return selectMove;
        }

        private MoveFight CheckCellsForRightMove(Figure figure)
        {
            MoveFight selectMove;

            if (figure.move.right != null)
            {
                selectMove = figure.move.right;
            }
            else
            {
                selectMove = figure.move.left;
            }

            return selectMove;
        }

        /// <summary>
        /// метод ходов игры. чередуют ходы белых и черных
        /// </summary>
        public void Game(string playerOne, string playerTwo)
        {       
            // ходы в цикле
            while (true)
            {
                if (isWhiteMove)
                {
                    UI.PrintNumberPlayer(playerOne);

                    Figure[] figuresToMove = GetWhiteMoves();

                    if (figuresToMove == null)
                    {
                        UI.PrintWinner(playerTwo);
                    }
                    Figure one = SelectFigureForMove(figuresToMove);

                    MoveFigure(one);

                    isWhiteMove = false;
                    
                }
                else
                {
                    UI.PrintNumberPlayer(playerTwo);

                    Figure[] figuresToMove = GetBlacksMoves();

                    if (figuresToMove == null)
                    {
                        UI.PrintWinner(playerOne);
                    }
                    
                    Figure one = SelectFigureForMove(figuresToMove);

                    MoveFigure(one);

                    isWhiteMove = true;
                    
                    
                }
            }
        }

       

    }
}