using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    /// <summary>
    /// клетка с фигурой, координатами и цветом
    /// </summary>
    public class Cell
    {
        public CellColor Color;
        public Figure Fig;
        public int x, y;
        
    }

    /// <summary>
    /// цвет клетки
    /// </summary>
    public enum CellColor
    {
        White,
        Black
        
    }
}
