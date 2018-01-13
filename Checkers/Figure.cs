using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    
    /// <summary>
    /// состояние фигуры
    /// </summary>
    public enum FigureState
    {
        None,
        White,
        WhiteQueen,
        Black,
        BlackQueen 
    }

    /// <summary>
    /// координата на шашечной доске
    /// </summary>
    public class Coordinate                     
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    /// координаты клеток, на которые можно походить
    /// </summary>
    public struct FigMove
    {
        public bool isMove;   // возможность хода в принципе

        public Coordinate left, right;
     
        public bool isFight;   // признак боя
        public int xbackleft, ybackleft;
        public int xbackright, ybackright;
    }


    /// <summary>
    /// фигура с координатами и ее состоянием
    /// </summary>
    public class Figure
    {
        public int x, y;
        public FigureState State = FigureState.None;
        public FigMove move;

        /// <summary>
        /// узнаем консольный цвет фигуры по ее состоянию
        /// </summary>
        /// <returns>цвет</returns>
        public ConsoleColor GetColorByState()
        {
            ConsoleColor cc = ConsoleColor.Black;

            if (State == FigureState.White || State == FigureState.WhiteQueen)
            {
                cc = ConsoleColor.White;
            }

            return cc;
        }
    }
}
