using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Carcassonne.Classes
{
    public class Card2 : CardBase
    {
        public Card2()
        {
            _edgeNorth = new CardEdge { HasCity = false, HasStreet = true, HasMeadow = true };
            _edgeEast = new CardEdge { HasCity = false, HasStreet = false, HasMeadow = true };
            _edgeSouth = new CardEdge { HasCity = false, HasStreet = false, HasMeadow = true };
            _edgeWest = new CardEdge { HasCity = false, HasStreet = true, HasMeadow = true };


            CardImage = new BitmapImage();

            CardImage.BeginInit();
            CardImage.UriSource = new Uri(@"D:\User_Data\Documents\Visual Studio 2015\Projects\Carcassonne\Images\Card2.jpg");
            CardImage.EndInit();
        }

        public override string ToString()
        {
            return "Card2";
        }

    }
}