using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Carcassonne.Classes
{
    public class Card3 : CardBase
    {
        public Card3()
        {
            EdgeNorth = new CardEdge { HasCity = false, HasStreet = false, HasMeadow = true };
            EdgeEast = new CardEdge { HasCity = false, HasStreet = true, HasMeadow = true };
            EdgeSouth = new CardEdge { HasCity = false, HasStreet = false, HasMeadow = true };
            EdgeWest = new CardEdge { HasCity = false, HasStreet = true, HasMeadow = true };


            CardImage = new BitmapImage();

            CardImage.BeginInit();
            CardImage.UriSource = new Uri(@"D:\User_Data\Documents\Visual Studio 2015\Projects\Carcassonne\Images\Card3.jpg");
            CardImage.EndInit();
        }

        public override string ToString()
        {
            return "Card3";
        }

    }
}
