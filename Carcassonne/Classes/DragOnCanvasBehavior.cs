using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Carcassonne.Classes
{
    public class DragOnCanvasBehavior
    {

        // Singleton pattern.
        private static DragOnCanvasBehavior _instance = new DragOnCanvasBehavior();

        private IDragDropHandler DropHandler { get; set; }
      
        // Using a DependencyProperty as the backing store for DropHandler.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropHandlerProperty =
            DependencyProperty.Register("DropHandler", typeof(IDragDropHandler), typeof(DragOnCanvasBehavior), new PropertyMetadata(0));


        private Point _mouseStartPosition;
        private Point _elementNewPosition;
        private Point _elementPosition;

        public static IDragDropHandler GetDropHandler(UIElement target)
        {
            return (IDragDropHandler)target.GetValue(DropHandlerProperty);
        }

        public static void SetDropHandler(UIElement target, IDragDropHandler value)
        {
            target.SetValue(DropHandlerProperty, value);
        }

        private static void OnDropHandlerChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement)sender;
            IDragDropHandler handler = (IDragDropHandler)(e.NewValue);

            // re-initialize the singleton (otherwise different items will have the same behavior).
            _instance = new DragOnCanvasBehavior();

            _instance.DropHandler = handler;

            if (_instance.DropHandler != null)
            {
                element.MouseLeftButtonDown += _instance.ElementOnMouseLeftButtonDown;
                
            }


            //element.MouseLeftButtonDown += 
        }


        private void ElementOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _mouseStartPosition = this.GetMousePositionFromMainWindow(mouseButtonEventArgs);
            ((UIElement)sender).CaptureMouse();
        }

        private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            UIElement element = (UIElement)sender;
            element.ReleaseMouseCapture();

            if (this.DropHandler != null)
                this.DropHandler.Dropped();
        }

        private void ElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            // don't do anything if no button is clicked (or there will be no handler).
            if (!((UIElement)sender).IsMouseCaptured || DropHandler == null)
                return;

            // Calculate element movement.
            Point mouseNewPos = GetMousePositionFromMainWindow(mouseEventArgs);
            Vector movement = (mouseNewPos - _mouseStartPosition);

            // Make sure the mouse has moved since the last time we were here (the MouseMove is run in a loop while the button is clicked, even if the mouse isn't moving).
            if (movement.Length > 0)
            {
                // save current mouse position.
                this._mouseStartPosition = mouseNewPos;

                // save the mouse movement.
                Point elementNewPos = _elementNewPosition + movement;
                _elementPosition = _elementNewPosition;

                // notify the viewmodel that the element has been moved.
                DropHandler.Moved(_elementNewPosition.X, _elementNewPosition.Y);
                 
            }
        }



        private Point GetMousePositionFromMainWindow(MouseEventArgs mouseEventArgs)
        {
            Window mainWindow = Application.Current.MainWindow;
            return mouseEventArgs.GetPosition(mainWindow);
        }

    }
}
