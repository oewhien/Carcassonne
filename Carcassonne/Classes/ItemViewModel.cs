using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using Fody.PropertyChanged;

namespace Carcassonne.Classes
{

    public class ItemViewModel : PropertyChangedBase, IDragDropHandler
    {
        private readonly IEventAggregator _events;

        public int Width { get; set; }
        public int Height { get; set; }
        public double X { get; set; }
        public double Y { get; set; }



        public ItemViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);
            Width = 100;
            Height = 100;
            X = 0;
            Y = 0;
        }


        public void Dropped()
        {
            throw new NotImplementedException();
        }

        public void Moved(double x, double y)
        {
            X = x;
            Y = y;
            //_events.PublishOnUIThread(new ItemMovedEvent(X, Y, Width, Height, ID));
        }
    }
}
