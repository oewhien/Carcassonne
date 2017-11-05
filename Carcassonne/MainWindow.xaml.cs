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
using System.Threading;
using System.Globalization;
using System.Windows.Markup;
using System.Resources;
using Carcassonne.Classes.Meeples;
using Carcassonne.Classes.Cards;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            MyScrollViewer.ScrollToHorizontalOffset(viewModel.BoardWidth / 2 - this.Width/2);
            MyScrollViewer.ScrollToVerticalOffset(viewModel.BoardHeight / 2 - this.Height/2);
        }

        private void ItemsControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                //Panel _panel = sender as Panel;
                //if (_panel == null)
                //    return;
                Point mousePos = e.GetPosition(boardItemControl);

                CardBase card = e.Data.GetData("Object") as CardBase;
                if (card != null)
                    CardDrop(card, mousePos);

                MeepleBase meeple = e.Data.GetData("Object") as MeepleBase;
                if (meeple != null)
                    MeepleDrop(meeple, mousePos);

                // Todo: add effects etc.
            }
        }

        private void CardDrop(CardBase card, Point mousePos)
        {
            double x = Math.Round((mousePos.X - CardBase.Width / 2) / CardBase.Width) * CardBase.Width;
            double y = Math.Round((mousePos.Y - CardBase.Height / 2) / CardBase.Height) * CardBase.Height;
            CardPosition2CardGridConverter pos2GridConv = new CardPosition2CardGridConverter();
            IntPoint gridPos = (IntPoint)pos2GridConv.Convert(new BindPoint(x, y), null, CardBase.Width, null);

            if (!viewModel.CanDropCard(gridPos, card))
                return;

            card.Position = new BindPoint(x, y);

            viewModel.AddCardToBoard(card);
        }

        private void MeepleDrop(MeepleBase meeple, Point mousePos)
        {
            double x = Math.Round((mousePos.X - CardBase.Width / 2) / CardBase.Width) * CardBase.Width;
            double y = Math.Round((mousePos.Y - CardBase.Height / 2) / CardBase.Height) * CardBase.Height;
            CardPosition2CardGridConverter pos2GridConv = new CardPosition2CardGridConverter();
            IntPoint gridPos = (IntPoint)pos2GridConv.Convert(new BindPoint(x, y), null, CardBase.Width, null);

            CardBase card = viewModel.MyCardGrid.GetCardAt(gridPos.Y, gridPos.X);
            if (card == null)
                return;

            if (card != viewModel.MyCardGrid.GetLastCard())
                return;
                // The meeple can only be dropped at the most recently card that was added to the board.

            card.Meeple = meeple;
            viewModel.CurrentPlayer.MeepleFromStackToBoard(meeple);                   
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

        private void Button_ClickNewGame(object sender, RoutedEventArgs e)
        {
            string startNewCoreText = Carcassonne.Resources.LanguageResources.ResourceManager.GetString("MsgBoxNewGameText");
            string startNewTitleText = Carcassonne.Resources.LanguageResources.ResourceManager.GetString("MsgBoxNewGameTitle");
            // Message box gets language from OS :-(, Cannot change language of buttons.

            MessageBoxResult result = MessageBox.Show(startNewCoreText, startNewTitleText, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
                viewModel.NewGame.Execute(null);

        }

        private void Image_MouseMoveMeeple(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (viewModel.CurrentPlayer.MeepleStack.Count == 0)
                    return;

                DataObject data = new DataObject();
                data.SetData("Object", viewModel.CurrentPlayer.MeepleStack[0]);

                

                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
    }
}
