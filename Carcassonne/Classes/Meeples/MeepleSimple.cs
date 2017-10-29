using Carcassonne.Classes.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Carcassonne.Classes.Meeples
{
    public class MeepleSimple : MeepleBase
    {
        public MeepleSimple(PlayerBase owner) : base(owner)
        {           
            MeepleImage.BeginInit();
            MeepleImage.UriSource = new Uri(@"D:\User_Data\Documents\Visual Studio 2015\Projects\Carcassonne\Images\Meeple.png");
            MeepleImage.EndInit();            
        }
    }
}
