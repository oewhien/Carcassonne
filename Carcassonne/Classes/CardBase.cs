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
        //public CardRotation RotationState { get; set; }

        private CardRotation _rotationState;

        

      
        public string Test { get; set; } = "test";
        
        public CardEdge _edgeNorth;
        public CardEdge _edgeEast;
        public CardEdge _edgeSouth;
        public CardEdge _edgeWest;

        public event PropertyChangedEventHandler PropertyChanged;

        public int GridPosRow { get; set; }
        public int GridPosCol { get; set; }
        public int Height { get; } = 100;
        public int Width { get; } = 100;


        public BitmapImage CardImage { get; set; }

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

        public CardBase()
        {
            RotationState = CardRotation.Deg0;
            Test = "Tete";
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
