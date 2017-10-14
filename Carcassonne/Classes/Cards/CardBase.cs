using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows;
using System.ComponentModel;

namespace Carcassonne.Classes
{
    public class CardBase : DependencyObject, INotifyPropertyChanged
    {        
        private CardRotation _rotationState;
                        
        public CardEdge _edgeNorth;
        public CardEdge _edgeEast;
        public CardEdge _edgeSouth;
        public CardEdge _edgeWest;

        public event PropertyChangedEventHandler PropertyChanged;

        private int _gridPosRow;
        private int _gridPosCol;

        private double _posOffsetX;
        private double _posOffsetY;

       

        public int Height { get; } = 100;
        public int Width { get; } = 100;

        private BitmapImage _cardImage;

        public BitmapImage CardImage {
            get { return _cardImage; }
            set {
                _cardImage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CardImage"));
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public CardRotation RotationState
        {
            get
            {
                return _rotationState;
            }

            set
            {
                RotateCard(value); //Important to do this before setting the new value!
                _rotationState = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RotationState"));
                
            }
        }

        public int GridPosRow
        {
            get
            {
                return _gridPosRow;
            }

            set
            {
                _gridPosRow = (int) Math.Round((value - Height/2) * 0.01)*100 + (int) PosOffsetY;
                OnPropertyChanged(new PropertyChangedEventArgs("GridPosRow"));
            }
        }

        public int GridPosCol
        {
            get
            {
                return _gridPosCol;
            }

            set
            {
                _gridPosCol = (int) Math.Round((value - Width/2) * 0.01)*100 + (int) PosOffsetX;
                OnPropertyChanged(new PropertyChangedEventArgs("GridPosCol"));
            }
        }

        public double PosOffsetX
        {
            get
            {
                return _posOffsetX;
            }

            set
            {
                _posOffsetX = value;
                GridPosCol = GridPosCol;    // Hack to do the update.
            }
        }

        public double PosOffsetY
        {
            get
            {
                return _posOffsetY;
            }

            set
            {
                _posOffsetY = value;
                GridPosRow = GridPosRow;
                
            }
        }

        public CardBase()
        {
            RotationState = CardRotation.Deg0;            
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

        public void RotateCard(CardRotation cardRotation)
        {
            CardEdge[] currentCardEdges = new CardEdge[] { _edgeNorth, _edgeEast, _edgeSouth, _edgeWest };
            CardEdge[] newCardEdges = new CardEdge[4];

            int currentRotation = (int)RotationState;
            int newRotation = (int)cardRotation;

            int diffRotation = newRotation - currentRotation;
            diffRotation = (diffRotation < 0) ? 4 - diffRotation : diffRotation;

            for (int i = 0; i < 4; i++)
            {
                int currentIndex = (i + diffRotation)%4;
                newCardEdges[i] = currentCardEdges[currentIndex];
            }
            _edgeNorth = newCardEdges[0];
            _edgeEast = newCardEdges[1];
            _edgeSouth = newCardEdges[2];
            _edgeWest = newCardEdges[3];

        }

        public void RotateCardLeft()
        {            
            RotationState = (CardRotation) (((int) RotationState - 1) % 4); 
        }

        public void RotateCardRight()
        {
            RotationState = (CardRotation)(((int)RotationState + 1) % 4);
        }



    }

    public enum CardRotation
    {        
        [Description("0°")]
        Deg0 = 0,
        [Description("90°")]
        Deg90 = 1,
        [Description("180°")]
        Deg180 = 2,
        [Description("270°")]
        Deg270 = 3
    }

    

}
