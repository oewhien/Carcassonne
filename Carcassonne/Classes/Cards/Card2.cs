using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Carcassonne.Classes.Cards
{
    public class Card2 : CardBase
    {
        public Card2()
        {
            EdgeNorth = new CardEdge { HasCity = false, HasStreet = true, HasMeadow = true };
            EdgeEast = new CardEdge { HasCity = false, HasStreet = false, HasMeadow = true };
            EdgeSouth = new CardEdge { HasCity = false, HasStreet = false, HasMeadow = true };
            EdgeWest = new CardEdge { HasCity = false, HasStreet = true, HasMeadow = true };

             
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