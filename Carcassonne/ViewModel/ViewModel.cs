using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Carcassonne.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Carcassonne.Classes.Helper;
using System.Windows.Input;

namespace Carcassonne.ViewModel
{


    public class ViewModel : DependencyObject, INotifyPropertyChanged
    {
        private const int numBoardPositionsRow = 100;
        private const int numBoardPositionsCol = 120;

        private int _centerRow = numBoardPositionsRow / 2;
        private int _centerCol = numBoardPositionsCol / 2;

        private bool[,] _occupiedBoardPositions = new bool[numBoardPositionsRow, numBoardPositionsRow];

        public ICommand RotateCurrentCardLeftCommand { get; set; }
        public ICommand RotateCurrentCardRightCommand { get; set; }
        public ICommand MoveBoardLeft { get; set; }
        public ICommand MoveBoardRight { get; set; }
        public ICommand MoveBoardUp { get; set; }
        public ICommand MoveBoardDown { get; set; }
        
        public double OffsetBoardX
        {
            get { return (double)GetValue(OffsetBoardXProperty); }
            set { SetValue(OffsetBoardXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetBoardX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetBoardXProperty =
            DependencyProperty.Register("OffsetBoardX", typeof(double), typeof(ViewModel), new PropertyMetadata(0.0));



        public double OffsetBoardY
        {
            get { return (double)GetValue(OffsetBoardYProperty); }
            set { SetValue(OffsetBoardYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetBoardY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetBoardYProperty =
            DependencyProperty.Register("OffsetBoardY", typeof(double), typeof(ViewModel), new PropertyMetadata(0.0));




        private double _offsetIncrement = 10;

        private CardDeck _myCardDeck;

        public string WindowTitle { get; } = "Carcassone";
               
        
        public ObservableCollection<CardBase> CardsOnBoard { get; }

        private CardRotation _rotState;
        public CardRotation RotState
        {
            get { return _rotState; }
            set {                
                _rotState = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RotState"));
                CurrentCard.RotationState = value;
            }
        }        

        public ObservableCollection<CardRotation> RotationItems
        {
            get { return new ObservableCollection<CardRotation> { CardRotation.Deg0, CardRotation.Deg90, CardRotation.Deg180, CardRotation.Deg270 }; }
            set {; }
        }

        private CardBase _currentCard;
        public CardBase CurrentCard
        {
            get
            {
                return _currentCard;
            }

            set
            {
                _currentCard = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentCard"));
            }
        }

        public void AddCardToBoard(CardBase card)
        {
            CardsOnBoard.Add(card);
            
            
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            RotateCurrentCardLeftCommand = new RelayCommand(RotateCurrentCardLeft, CanRotatCurrent);
            RotateCurrentCardRightCommand = new RelayCommand(RotateCurrentCardRight, CanRotatCurrent);
            MoveBoardLeft = new RelayCommand(MoveBoardLeftAction);
            MoveBoardRight = new RelayCommand(MoveBoardRightAction);
            MoveBoardUp = new RelayCommand(MoveBoardUpAction);
            MoveBoardDown = new RelayCommand(MoveBoardDownAction);

           
            _myCardDeck = new CardDeck();

            CurrentCard = _myCardDeck.DrawCard();

            CardBase card0 = _myCardDeck.DrawCard();
            card0.GridPosRow = 100;
            card0.GridPosCol = 100;

            CardBase card1 = _myCardDeck.DrawCard();
            card1.GridPosRow = 100;
            card1.GridPosCol = 0;
            CardBase card2 = _myCardDeck.DrawCard();
            card2.GridPosRow = 200;
            card2.GridPosCol = 200;


            CardsOnBoard = new ObservableCollection<CardBase>() {};
            CardsOnBoard.CollectionChanged += CardsOnBoard_CollectionChanged;

            AddCardToBoard(card0);
            AddCardToBoard(card1);
            AddCardToBoard(card2);

        }

        private void MoveBoardLeftAction(object param)
        {
            OffsetBoardX -= _offsetIncrement;
            
            UpDateCardPositions();
        }
        private void MoveBoardRightAction(object param)
        {
            OffsetBoardX += _offsetIncrement;
            
            UpDateCardPositions();
        }
        private void MoveBoardUpAction(object param)
        {
            OffsetBoardY -= _offsetIncrement;
            
            UpDateCardPositions();
        }
        private void MoveBoardDownAction(object param)
        {
            OffsetBoardY += _offsetIncrement;
            
            UpDateCardPositions();
        }

        private void UpDateCardPositions()
        {
            foreach (CardBase card in CardsOnBoard)
            {
                card.PosOffsetX = OffsetBoardX;
                card.PosOffsetY = OffsetBoardY;
            }
        }

        private void CardsOnBoard_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                DrawNewCard();                
            }
        }

        private void DrawNewCard()
        {            
            CurrentCard = _myCardDeck.DrawCard();
            CurrentCard.GridPosRow = 0;
            CurrentCard.GridPosCol = 0;
            CurrentCard.RotationState = CardRotation.Deg0;
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        private void RotateCurrentCardLeft(object param)
        {
            CurrentCard.RotateCardLeft();
        }
        private void RotateCurrentCardRight(object param)
        {
            CurrentCard.RotateCardRight();
        }
        private bool CanRotatCurrent(object param)
        {
            return true;
        }
       
    }
}
