using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Carcassonne.Classes
{
    public class Card1 : CardBase
    {
        public Card1()
        {
            _edgeNorth = new CardEdge { HasCity = true, HasStreet = false, HasMeadow = false };
            _edgeEast = new CardEdge { HasCity = false, HasStreet = true, HasMeadow = true };
            _edgeSouth = new CardEdge { HasCity = true, HasStreet = false, HasMeadow = false };
            _edgeWest = new CardEdge { HasCity = true, HasStreet = false, HasMeadow = false };


            CardImage = new BitmapImage();

            CardImage.BeginInit();
            CardImage.UriSource = new Uri(@"D:\User_Data\Documents\Visual Studio 2015\Projects\Carcassonne\Images\Card1.jpg");
            CardImage.EndInit();
        }

        public override string ToString()
        {
            return "Card1";
        }

    }
}
