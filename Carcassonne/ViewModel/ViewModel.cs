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
using System.Runtime.CompilerServices;
using Carcassonne.Converter;

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

        private CardPosition2CardGridConverter pos2GridConv = new CardPosition2CardGridConverter();

        private CardDeck _myCardDeck;
        public CardGrid MyCardGrid;

        public string WindowTitle { get; } = "Carcassone";
               
        
        public ObservableCollection<CardBase> CardsOnBoard { get; }

        private CardRotation _rotState;
        public CardRotation RotState
        {
            get { return _rotState; }
            set {                
                _rotState = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
            MyCardGrid = new CardGrid();


            _myCardDeck = new CardDeck();

            CurrentCard = _myCardDeck.DrawCard();

            CardBase card0 = _myCardDeck.DrawCard();
            card0.Position = new BindPoint(100.0,100.0);            

            CardBase card1 = _myCardDeck.DrawCard();
            card1.Position = new BindPoint(100.0, 0.0);
            
            CardBase card2 = _myCardDeck.DrawCard();
            card2.Position = new BindPoint(200.0, 200.0);

            IntPoint gridPos = (IntPoint) pos2GridConv.Convert(card0.Position, null, card0.Width, null);

            CardsOnBoard = new ObservableCollection<CardBase>() {};
            CardsOnBoard.CollectionChanged += CardsOnBoard_CollectionChanged;

            AddCardToBoard(card0);
            AddCardToBoard(card1);
            AddCardToBoard(card2);
            MyCardGrid.FillGridPos(card0);
            MyCardGrid.FillGridPos(card1);
            MyCardGrid.FillGridPos(card2);

            bool isOc = MyCardGrid.IsOccupied(1,1);
            bool isOc2 = MyCardGrid.IsNeighbourOccupied(1, 0);

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
            
            CurrentCard.RotationState = CardRotation.Deg0;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
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
