using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Carcassonne.Classes
{
    public class CardBase
    {
        public Rotation RotationState { get; set; }

        public CardEdge _edgeNorth;
        public CardEdge _edgeSouth;
        public CardEdge _edgeWest;
        public CardEdge _edgeEast;

        public int GridPosRow { get; set; }
        public int GridPosCol { get; set; }

        public BitmapImage CardImage { get; set; }

        public CardBase()
        {
            RotationState = Rotation.Deg0;
            GridPosRow = -1;
            GridPosCol = -1;        
        }

        public int RotAngle()
        {
            return (int)RotationState*90; 
        }

        public class CardEdge
        {
            public bool HasStreet;
            public bool HasCity;
            public bool HasMeadow;
        }
    }

    public enum Rotation
    {
        Deg0 = 0,
        Deg90 = 1,
        Deg180 = 2,
        Deg270 = 3
    }

    

}
