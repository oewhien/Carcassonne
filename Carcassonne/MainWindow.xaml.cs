using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Carcassonne.Classes;
using Carcassonne.Converter;
using Carcassonne.Classes.Helper;

namespace Carcassonne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ItemsControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                //Panel _panel = sender as Panel;
                //if (_panel == null)
                //    return;
                CardBase _card = e.Data.GetData("Object") as CardBase;
                if (_card == null)
                    return;
                
                Point mousePos = e.GetPosition(boardItemControl);

                double x = Math.Round((mousePos.X - _card.Width/2)/_card.Width)*_card.Width;
                double y = Math.Round((mousePos.Y - _card.Height/2)/_card.Height)*_card.Height;
                CardPosition2CardGridConverter pos2GridConv = new CardPosition2CardGridConverter();
                IntPoint gridPos = (IntPoint) pos2GridConv.Convert(new BindPoint(x, y), null, _card.Width, null);


                if (!viewModel.MyCardGrid.IsNeighbourOccupied(gridPos.Y, gridPos.X))
                    return;

                _card.Position.X = x;
                _card.Position.Y = y;

                viewModel.AddCardToBoard(_card);
                // Todo: add effects etc.
            }
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("Object", viewModel.CurrentCard);

                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }

            
        }




    }
}
