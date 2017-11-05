using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Carcassonne.Classes.Cards
{
    public class Card0 : CardBase
    {
        public Card0()
        {
            EdgeNorth = new CardEdge { HasCity = false, HasStreet = true, HasMeadow = true };
            EdgeEast = new CardEdge { HasCity = false, HasStreet = false, HasMeadow = true };
            EdgeSouth = new CardEdge { HasCity = false, HasStreet = false, HasMeadow = true };
            EdgeWest = new CardEdge { HasCity = true, HasStreet = false, HasMeadow = false };


            SetImage(@"D:\User_Data\Documents\Visual Studio 2015\Projects\Carcassonne\Images\Card0.jpg");
            //SetMask(@"D:\User_Data\Documents\Visual Studio 2015\Projects\Carcassonne\Images\Card0Mask.txt");
        }



    }
}
