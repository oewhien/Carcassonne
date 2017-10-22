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

        private bool[,] _occupiedBoardPositions;

        public ICommand RotateCurrentCardLeftCommand { get; set; }
        public ICommand RotateCurrentCardRightCommand { get; set; }
        public ICommand NewGame { get; set; }

        private CardPosition2CardGridConverter pos2GridConv = new CardPosition2CardGridConverter();

        private CardDeck _myCardDeck;
        public CardGrid MyCardGrid;                    
      
        public ObservableCollection<CardBase> CardsOnBoard { get;}

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
            MyCardGrid.FillGridPos(card);            
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            RotateCurrentCardLeftCommand = new RelayCommand(RotateCurrentCardLeft, CanRotatCurrent);
            RotateCurrentCardRightCommand = new RelayCommand(RotateCurrentCardRight, CanRotatCurrent);
            NewGame = new RelayCommand(NewGameAction, CanNewGameAction);

            CardsOnBoard = new ObservableCollection<CardBase>() { };
            CardsOnBoard.CollectionChanged += CardsOnBoard_CollectionChanged;

            IntializeGame();

        }
        
        private void IntializeGame()
        {
            CardsOnBoard.Clear();
            _occupiedBoardPositions = new bool[numBoardPositionsRow, numBoardPositionsRow];

            MyCardGrid = new CardGrid();
            _myCardDeck = new CardDeck();


            CurrentCard = _myCardDeck.DrawCard();

            CardBase card0 = _myCardDeck.DrawCard();
            card0.Position = new BindPoint(100.0, 100.0);

            CardBase card1 = _myCardDeck.DrawCard();
            card1.Position = new BindPoint(100.0, 0.0);

            CardBase card2 = _myCardDeck.DrawCard();
            card2.Position = new BindPoint(200.0, 200.0);

            IntPoint gridPos = (IntPoint)pos2GridConv.Convert(card0.Position, null, card0.Width, null);
            

            

            AddCardToBoard(card0);
            AddCardToBoard(card1);
            AddCardToBoard(card2);
        }


        private void NewGameAction(object para)
        {
            IntializeGame();
        }

        private bool CanNewGameAction(object para)
        {
            return true;
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
