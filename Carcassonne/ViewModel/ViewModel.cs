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

        public ICommand RotateCurrentCardLeftCommand { get; set; }
        public ICommand RotateCurrentCardRightCommand { get; set; }
        private CardDeck _myCardDeck;

        public string WindowTitle { get; } = "Carcassone";

        public ObservableCollection<CardBase> CardsOnBoard { get; set; }

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

        //public CardBase CurrentCard
        //{
        //    get { return (CardBase)GetValue(CurrentCardProperty); }
        //    set { SetValue(CurrentCardProperty, value); }
        //}

        //public static FrameworkPropertyMetadata currentCardPropMeta = new FrameworkPropertyMetadata()
        //{
        //    AffectsRender = true
        //};        

        //// Using a DependencyProperty as the backing store for CurrentCard.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CurrentCardProperty =
        //    DependencyProperty.Register("CurrentCard", typeof(CardBase), typeof(MainWindow), currentCardPropMeta);

        
        



        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            RotateCurrentCardLeftCommand = new RelayCommand(RotateCurrentCardLeft, CanRotatCurrent);
            RotateCurrentCardRightCommand = new RelayCommand(RotateCurrentCardRight, CanRotatCurrent);

            _myCardDeck = new CardDeck();

            CurrentCard = _myCardDeck.DrawCard();

            CardBase card1 = _myCardDeck.DrawCard();
            card1.GridPosRow = 0;
            card1.GridPosCol = 100;
            CardBase card2 = _myCardDeck.DrawCard();
            card2.GridPosRow = 0;
            card2.GridPosCol = 200;
            CardBase card3 = _myCardDeck.DrawCard();
            card3.GridPosRow = 100;
            card3.GridPosCol = 200;

            CardsOnBoard = new ObservableCollection<CardBase>() { card1, card2, card3 };
            CardsOnBoard.CollectionChanged += CardsOnBoard_CollectionChanged;
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
            CurrentCard.GridPosCol = 0;
            CurrentCard.GridPosRow = 0;
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
