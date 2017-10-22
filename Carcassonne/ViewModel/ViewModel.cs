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
using Carcassonne.Classes.Player;

namespace Carcassonne.ViewModel
{


    public class ViewModel : DependencyObject, INotifyPropertyChanged
    {
        private const int _cardWidth = 100;
        private const int _cardHeight = 100;

        private const int _numBoardPositionsRow  = 100;
        private const int _numBoardPositionsCol  = 120;

        public int BoardWidth
        {
            get { return _numBoardPositionsCol * _cardWidth; }
        }
        public int BoardHeight
        {
            get { return _numBoardPositionsRow * _cardHeight;  }
        }

        public int CenterRow { get; } = _numBoardPositionsRow / 2;
        public int CenterCol { get; } = _numBoardPositionsCol / 2;

        private bool[,] _occupiedBoardPositions;

        public ICommand RotateCurrentCardLeftCommand { get; set; }
        public ICommand RotateCurrentCardRightCommand { get; set; }
        public ICommand NewGame { get; set; }

        private CardPosition2CardGridConverter pos2GridConv = new CardPosition2CardGridConverter();

        private CardDeck _myCardDeck;
        public CardGrid MyCardGrid;

        private PlayerBase _playerHuman;
        public PlayerBase PlayerHuman {
            get { return _playerHuman; }
            set {
                _playerHuman = value;
                OnPropertyChanged();
            }
        }

        private PlayerBase _playerPc;
        public PlayerBase PlayerPc {
            get { return _playerPc; }
            set
            {
                _playerPc = value;
                OnPropertyChanged();
            }
        }

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

            CardBase.Width = _cardWidth;
            CardBase.Height = _cardHeight;

            IntializeGame();
            
        }
        
        private void IntializeGame()
        {
            CardsOnBoard.Clear();
            _occupiedBoardPositions = new bool[_numBoardPositionsRow, _numBoardPositionsRow];

            MyCardGrid = new CardGrid();
            _myCardDeck = new CardDeck();


            CurrentCard = _myCardDeck.DrawCard();

            CardBase card0 = _myCardDeck.DrawCard();
            card0.Position = new BindPoint(_cardWidth * CenterCol, _cardHeight * CenterRow);
          

            IntPoint gridPos = (IntPoint)pos2GridConv.Convert(card0.Position, null, _cardWidth, null);         

            AddCardToBoard(card0);

            PlayerHuman = new PlayerBase();
            PlayerPc = new PlayerBase();
        }

        public bool CanDropCard(IntPoint gridPos, CardBase card)
        {
            CardNeighbours neighbours = MyCardGrid.GetNeighbours(gridPos.Y, gridPos.X);
            if (neighbours.NumNeighbours() == 0)
                return false;

            if (neighbours.North != null)
                if (!CardEdgesEqual(neighbours.North.EdgeSouth, card.EdgeNorth))
                    return false;
            if (neighbours.East != null)
                if (!CardEdgesEqual(neighbours.East.EdgeWest, card.EdgeEast))
                    return false;
            if (neighbours.West != null)
                if (!CardEdgesEqual(neighbours.West.EdgeEast, card.EdgeWest))
                    return false;
            if (neighbours.South != null)
                if (!CardEdgesEqual(neighbours.South.EdgeNorth, card.EdgeSouth))
                    return false;

            return true;
        }

        private bool CardEdgesEqual(CardEdge edge0, CardEdge edge1)
        {
            if (edge0.HasCity != edge1.HasCity)
                return false;
            if (edge0.HasStreet != edge1.HasStreet)
                return false;
            if (edge0.HasMeadow != edge1.HasMeadow)
                return false;

            return true;
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
