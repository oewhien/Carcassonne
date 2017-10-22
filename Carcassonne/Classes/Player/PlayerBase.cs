using Carcassonne.Classes.Meeples;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Carcassonne.Classes.Player
{
    public class PlayerBase : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MeepleBase> MeepleStack { get; set; }
        public ObservableCollection<MeepleBase> MeeplesOnBoard { get; set; }

        public PlayerBase()
        {
            MeepleStack = new ObservableCollection<MeepleBase>();
            MeeplesOnBoard = new ObservableCollection<MeepleBase>();

            MeepleStack.Add(new MeepleSimple());
        }

    }
}
