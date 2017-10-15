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


           
            _myCardDeck = new CardDeck();

            CurrentCard = _myCardDeck.DrawCard();

            CardBase card0 = _myCardDeck.DrawCard();
            card0.Position = new BindPoint(100.0,100.0);
            

            //CardBase card1 = _myCardDeck.DrawCard();
            //card1.GridPosRow = 100;
            //card1.GridPosCol = 0;
            //CardBase card2 = _myCardDeck.DrawCard();
            //card2.GridPosRow = 200;
            //card2.GridPosCol = 200;


            CardsOnBoard = new ObservableCollection<CardBase>() {};
            CardsOnBoard.CollectionChanged += CardsOnBoard_CollectionChanged;

            AddCardToBoard(card0);
            //AddCardToBoard(card1);
            //AddCardToBoard(card2);

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
            //CurrentCard.GridPosRow = 0;
            //CurrentCard.GridPosCol = 0;
            CurrentCard.GridPosition = new IntPoint(0, 0);
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
