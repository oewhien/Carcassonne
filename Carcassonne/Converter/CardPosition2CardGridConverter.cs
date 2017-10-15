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
    class CardPosition2CardGridConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(Point))
                throw new InvalidCastException("Input is not valid Point.");

            if (parameter == null)
                parameter = 1;
            else if (parameter.GetType() != typeof(int))
                throw new InvalidCastException("Parameter is not a valid int.");

            int size = (int) parameter;

            Point point = (Point) value;
            int x = (int) point.X / size;
            int y = (int) point.Y / size; 

            IntPoint intPoint = new IntPoint(x,y);

            return intPoint;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
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

            Point point = new Point(x, y);

            return point;
            
        }
    }
}
