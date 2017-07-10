using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Element
    {
        #region Private Variables

        private ElementState _elementState;
        private int _row;
        private int _column;

        #endregion

        #region Public Properties

        public ElementState ElementState { get { return _elementState; } set { _elementState = value; } }
        public int Row { get { return _row; } set { _row = value; } }
        public int Column { get { return _column; } set { _column = value; } }

        #endregion

        #region Constructor

        public Element(int row, int column, ElementState elementState) 
        {
            _row = row;
            _column = column;
            _elementState = elementState;
        }

        #endregion

    }
}
