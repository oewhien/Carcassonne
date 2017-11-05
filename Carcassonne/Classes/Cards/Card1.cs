using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Carcassonne.Classes.Cards
{
    public class Card1 : CardBase
    {
        public Card1()
        {
            EdgeNorth = new CardEdge { HasCity = true, HasStreet = false, HasMeadow = false };
            EdgeEast = new CardEdge { HasCity = false, HasStreet = true, HasMeadow = true };
            EdgeSouth = new CardEdge { HasCity = true, HasStreet = false, HasMeadow = false };
            EdgeWest = new CardEdge { HasCity = true, HasStreet = false, HasMeadow = false };

            SetImage(@"D:\User_Data\Documents\Visual Studio 2015\Projects\Carcassonne\Images\Card1.jpg");
        }



    }
}
