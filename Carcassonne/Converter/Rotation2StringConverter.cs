using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Carcassonne.Classes;
using System.Reflection;
using System.ComponentModel;
using System.Windows;

namespace Carcassonne.Converter
{
    public class Rotation2StringConverter : IValueConverter
    {
        //[ValueConversion(typeof(CardRotation),typeof(string))]
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            return GetDescription((CardRotation)value);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string GetDescription(CardRotation rotation)
        {
            Type type = rotation.GetType();
            MemberInfo[] memInfo = type.GetMember(type.ToString());

            if(memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return rotation.ToString();
        }

    }
}
