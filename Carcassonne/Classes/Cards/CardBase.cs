using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows;
using System.ComponentModel;
using Carcassonne.Classes.Helper;
using Carcassonne.Converter;

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

        public CardPosition2CardGridConverter Pos2GridConv { get; }

        public BindPoint Position
        {
            get { return (BindPoint)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        private static readonly PropertyMetadata posMeta = new PropertyMetadata()
        {
            PropertyChangedCallback = OnPosChanged
        };

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(BindPoint), typeof(CardBase), posMeta);


        private static void OnPosChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            CardBase card = sender as CardBase;
            if (card != null)
            {                
                IntPoint intP = (IntPoint) card.Pos2GridConv.Convert(args.NewValue, null, card.Width, null);
                card.GridPosition = intP;
            }
        }

        public IntPoint GridPosition;

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

      

        public CardBase()
        {
            Pos2GridConv = new CardPosition2CardGridConverter();
            RotationState = CardRotation.Deg0;
            GridPosition = new IntPoint(-1, -1);
            Position = new BindPoint();
                 
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
