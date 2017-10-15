using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Classes.Helper
{
    public class BindPoint : INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        

        public BindPoint()
        {
            X = 0.0;
            Y = 0.0;
        }

        public BindPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X
        {
            get
            {
                return _x;
            }

            set
            {
                if (value.Equals(_x))
                    return;
                _x = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }

            set
            {
                if (value.Equals(_y))
                    return;
                _y = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
