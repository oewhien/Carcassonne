using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Classes.Helper
{
    public class IntPoint
    {
        private int _x;
        private int _y;
        private const int _roundingDigits = 100;
        

        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double XD
        {
            get { return (double)_x; }
            set { _x = (int) Math.Round((value) / _roundingDigits) * _roundingDigits; }
        }

        public double YD
        {
            get { return (double)_y; }
            set { _y = (int)Math.Round((value) / _roundingDigits) * _roundingDigits; }
        }

        public int X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }
        }
    }
}
