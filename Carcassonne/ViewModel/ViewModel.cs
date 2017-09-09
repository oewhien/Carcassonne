using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Carcassonne.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Carcassonne.ViewModel
{

    public class RectItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class ViewModel : DependencyObject, INotifyPropertyChanged
    {

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

        public string TestString
        {
            get { return (string)GetValue(TestStringProperty); }
            set { SetValue(TestStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestStringProperty =
            DependencyProperty.Register("TestString", typeof(string), typeof(MainWindow), new PropertyMetadata("1, 1, Test"));




        public CardBase CurrentCard
        {
            get { return (CardBase)GetValue(CurrentCardProperty); }
            set { SetValue(CurrentCardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentCard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentCardProperty =
            DependencyProperty.Register("CurrentCard", typeof(CardBase), typeof(MainWindow), new PropertyMetadata());

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            CurrentCard = new Card1();

            CardBase card0 = new Card1()
            {
                GridPosRow = 0,
                GridPosCol = 0,

            };

            CardBase card1 = new Card1()
            {
                GridPosRow = 100,
                GridPosCol = 100,
                RotationState = CardRotation.Deg270
            };


            CardsOnBoard = new ObservableCollection<CardBase>() { card0, card1 };

        }


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
       
    }
}
