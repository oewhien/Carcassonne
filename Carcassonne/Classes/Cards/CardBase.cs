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
using Carcassonne.Classes.Meeples;
using System.Runtime.CompilerServices;

namespace Carcassonne.Classes.Cards
{
    public class CardBase : DependencyObject, INotifyPropertyChanged
    {
        private CardRotation _rotationState;

        public CardEdge EdgeNorth;
        public CardEdge EdgeEast;
        public CardEdge EdgeSouth;
        public CardEdge EdgeWest;

        public event PropertyChangedEventHandler PropertyChanged;

        public CardPosition2CardGridConverter Pos2GridConv { get; }

        public BindPoint Position
        {
            get { return (BindPoint)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        private static readonly PropertyMetadata posMeta = new PropertyMetadata()
        {
            PropertyChangedCallback = OnPosChanged,            
        };

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(BindPoint), typeof(CardBase), posMeta);


        private static void OnPosChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            CardBase card = sender as CardBase;
            if (card != null)
            {                
                IntPoint intP = (IntPoint) card.Pos2GridConv.Convert(args.NewValue, null, Width, null);
                card.GridPosition = intP;
            }
        }

        public IntPoint GridPosition;

        public static int Height { get; set; } = 100;
        public static int Width { get; set; } = 100;

        private BitmapImage _cardImage;

        public BitmapImage CardImage {
            get { return _cardImage; }
            set {
                _cardImage = value;
                OnPropertyChanged();
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
            
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
                OnPropertyChanged();
                
            }
        }

        public MeepleBase Meeple
        {
            get
            {
                return _meeple;
            }

            set
            {
                _meeple = value;
                OnPropertyChanged();                
            }
        }

        private MeepleBase _meeple;




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

        

        public void RotateCard(CardRotation cardRotation)
        {           
            CardEdge[] currentCardEdges = new CardEdge[] { EdgeNorth, EdgeEast, EdgeSouth, EdgeWest };
            CardEdge[] newCardEdges = new CardEdge[4];

            int currentRotation = (int)RotationState;
            int newRotation = (int)cardRotation;

            //int diffRotation = newRotation - currentRotation;            
            int diffRotation =  currentRotation - newRotation;
            diffRotation = (diffRotation < 0) ? 4 + diffRotation : diffRotation;

            //Console.WriteLine("Diffrot = {0}", diffRotation);
            for (int i = 0; i < 4; i++)
            {
                
                int currentIndex = (i + diffRotation)%4;
                newCardEdges[i] = currentCardEdges[currentIndex];
                //Console.WriteLine("{0} -> {1}", i, currentIndex);
            }
            EdgeNorth = newCardEdges[0];
            EdgeEast = newCardEdges[1];
            EdgeSouth = newCardEdges[2];
            EdgeWest = newCardEdges[3];

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

    public class CardEdge
    {
        public bool HasStreet;
        public bool HasCity;
        public bool HasMeadow;
    }

}
