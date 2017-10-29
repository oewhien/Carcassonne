using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Carcassonne.Classes;
using Carcassonne.Classes.Cards;

namespace Carcassonne.Converter
{
    [ValueConversion(typeof(CardBase), typeof(Image))]
    public class Card2VisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CardBase card = value as CardBase;
            Image image = new Image();
            if (card != null)
            {
                BitmapImage bitmap = card.CardImage;   

                RotateTransform rotTrans = new RotateTransform(card.RotAngle());
                
                TransformedBitmap bitmapTrans = new TransformedBitmap();
                bitmapTrans.BeginInit();
                bitmapTrans.Source = bitmap;
                
                
                bitmapTrans.Transform = rotTrans;
                bitmapTrans.EndInit();

                image = new Image();
                image.Source = bitmapTrans;
                image.Width = 100;
                image.Height = 100;

            }           

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
