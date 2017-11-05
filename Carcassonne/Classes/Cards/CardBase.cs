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
using System.IO;

namespace Carcassonne.Classes.Cards
{
    public class CardBase : DependencyObject, INotifyPropertyChanged
    {
        private CardRotation _rotationState;

        public CardEdge EdgeNorth;
        public CardEdge EdgeEast;
        public CardEdge EdgeSouth;
        public CardEdge EdgeWest;
        public CardMasks MyCardMask;


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
            private set {
                _cardImage = value;
                OnPropertyChanged();
            }
        }

        protected void SetImage(string path)
        {
            CardImage = new BitmapImage();
            CardImage.BeginInit();
            CardImage.UriSource = new Uri(path);
            CardImage.EndInit();
        }

        protected void SetMask(string path)
        {
            CardMasks mask = new CardMasks();

            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("Meadows"))
                {
                    mask.Meadows = GetMaskArray(lines, i + 1);
                    i = i + 4;                    
                    continue;
                }
                if (line.Contains("Monastery"))
                {
                    mask.Monastery = GetMaskArray(lines, i + 1);
                    i = i + 4;                    
                    continue;
                }
                if (line.Contains("Streets"))
                {
                    mask.Streets = GetMaskArray(lines, i + 1);
                    i = i + 4;
                    continue;
                }
                if (line.Contains("Cities"))
                {
                    mask.Cities = GetMaskArray(lines, i + 1);
                    i = i + 4;
                    continue;
                }
                if (line.Contains("Grain"))
                {
                    mask.Grain = GetMaskArray(lines, i + 1);
                    i = i + 4;
                    continue;
                }
                if (line.Contains("Fabric"))
                {
                    mask.Fabric = GetMaskArray(lines, i + 1);
                    i = i + 4;
                    continue;
                }
                if (line.Contains("Barrel"))
                {
                    mask.Barrel = GetMaskArray(lines, i + 1);
                    i = i + 4;
                    continue;
                }
                if (line.Contains("Shield"))
                {
                    mask.Shield = GetMaskArray(lines, i + 1);
                    i = i + 4;
                    continue;
                }
            }
            MyCardMask = mask;
            SetEdges(mask);
        }

        private void SetEdges(CardMasks mask)
        {
            if (mask.Meadows[0, 1] != 0)
                EdgeNorth.HasMeadow = true;
            if (mask.Cities[0, 1] != 0)
                EdgeNorth.HasCity = true;
            if (mask.Streets[0, 1] != 0)
                EdgeNorth.HasStreet = true;

            if (mask.Meadows[1, 0] != 0)
                EdgeWest.HasMeadow = true;
            if (mask.Cities[1, 0] != 0)
                EdgeWest.HasCity = true;
            if (mask.Streets[1, 0] != 0)
                EdgeWest.HasStreet = true;

            if (mask.Meadows[1, 2] != 0)
                EdgeEast.HasMeadow = true;
            if (mask.Cities[1, 2] != 0)
                EdgeEast.HasCity = true;
            if (mask.Streets[1, 2] != 0)
                EdgeEast.HasStreet = true;

            if (mask.Meadows[2, 1] != 0)
                EdgeSouth.HasMeadow = true;
            if (mask.Cities[2, 1] != 0)
                EdgeSouth.HasCity = true;
            if (mask.Streets[2, 1] != 0)
                EdgeSouth.HasStreet = true;
        }


        private int[,] GetMaskArray(string[] lines, int startInd)
        {
            int[,] maskArray = new int[3, 3];

            for (int i = 0; i < 3; i++)
            {
                int lineInd = startInd + i;
                string line = lines[lineInd];
                string[] chars = line.Split('\t');

                Int32.TryParse(chars[0], out maskArray[i, 0]);
                Int32.TryParse(chars[1], out maskArray[i, 1]);
                Int32.TryParse(chars[2], out maskArray[i, 2]);
            }

            return maskArray;
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
            
            if (diffRotation == 0)
                return;

            Console.WriteLine("Diffrot = {0}", diffRotation);
            for (int i = 0; i < 4; i++)
            {                
                int currentIndex = (i + diffRotation)%4;
                newCardEdges[i] = currentCardEdges[currentIndex];
                Console.WriteLine("{0} -> {1}", i, currentIndex);                
            }
            EdgeNorth = newCardEdges[0];
            EdgeEast = newCardEdges[1];
            EdgeSouth = newCardEdges[2];
            EdgeWest = newCardEdges[3];

            RotateMask(diffRotation);

        }

        private void RotateMask(int diffRotation)
        {
            for (int i=0; i < diffRotation; i++)
            {
                MyCardMask.Meadows = RotateArray90DegClckw(MyCardMask.Meadows);
                // MyCardMask.Monastery = RotateArray90DegClckw(MyCardMask.Monastery);  //Monastery is always in the center and thus needs no rotation.
                MyCardMask.Streets = RotateArray90DegClckw(MyCardMask.Streets);
                MyCardMask.Cities = RotateArray90DegClckw(MyCardMask.Cities);
                MyCardMask.Grain = RotateArray90DegClckw(MyCardMask.Grain);
                MyCardMask.Fabric = RotateArray90DegClckw(MyCardMask.Fabric);
                MyCardMask.Barrel = RotateArray90DegClckw(MyCardMask.Barrel);
                MyCardMask.Shield = RotateArray90DegClckw(MyCardMask.Shield);
            }
        }

        private int[,] RotateArray90DegClckw(int[,] inArray)
        {
            int[,] outArray = new int[3,3];
            outArray[0, 1] = inArray[1, 0];
            outArray[1, 2] = inArray[0, 1];
            outArray[2, 1] = inArray[1, 2];
            outArray[1, 0] = inArray[2, 1];
            outArray[1, 1] = inArray[1, 1];

            return outArray;
        }

        public void RotateCardLeft()
        {            
            RotationState = (CardRotation) (((int) RotationState - 1) % 4); 
        }

        public void RotateCardRight()
        {
            RotationState = (CardRotation)(((int)RotationState + 1) % 4);
        }

        public override string ToString()
        {
            return this.GetType().Name;
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

    public class CardMasks
    {
        public int[,] Meadows;
        public int[,] Monastery;
        public int[,] Streets;
        public int[,] Cities;
        public int[,] Grain;
        public int[,] Fabric;
        public int[,] Barrel;
        public int[,] Shield;
    }


}
