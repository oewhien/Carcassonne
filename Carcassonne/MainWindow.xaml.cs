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

                _card.GridPosRow = (int) mousePos.Y;
                _card.GridPosCol = (int) mousePos.X;

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
