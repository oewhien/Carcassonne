using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Carcassonne.Classes.Meeples
{
    public class MeepleBase : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BitmapImage MeepleImage { get; }
        

        public MeepleBase()
        {
            MeepleImage = new BitmapImage();
        }

    }
}
