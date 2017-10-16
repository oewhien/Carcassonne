using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Carcassonne.Classes.Helper;

namespace Carcassonne.Converter
{
    [ValueConversion(typeof(Point),typeof(IntPoint))]    
    public class CardPosition2CardGridConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(BindPoint))
                throw new InvalidCastException("Input is not valid BindPoint.");

            if (parameter == null)
                parameter = 1;
            else if (parameter.GetType() != typeof(int))
                throw new InvalidCastException("Parameter is not a valid int.");

            int size = (int) parameter;

            BindPoint point = (BindPoint) value;
            int x = (int) point.X / size;
            int y = (int) point.Y / size; 

            IntPoint intPoint = new IntPoint(x,y);

            return intPoint;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(IntPoint))
                throw new InvalidCastException("Input is not a valid IntPoint.");

            if (parameter == null)
                parameter = 1;
            else if (parameter.GetType() != typeof(int))
                throw new InvalidCastException("Parameter is not a valid int.");

            int size = (int) parameter;
            IntPoint intPoint = (IntPoint) value;

            double x = intPoint.X * size;
            double y = intPoint.Y * size;

            BindPoint point = new BindPoint(x, y);

            return point;
            
        }
    }
}
