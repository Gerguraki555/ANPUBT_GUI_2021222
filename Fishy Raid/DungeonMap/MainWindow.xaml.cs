using System;
using System.Collections.Generic;
using System.IO;
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
using BeforeFightMenu;

namespace DungeonMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            #region Geting Background Path for grid
            string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName+"/Images", "DungeonMap.jpg");
            ImageBrush backgroundBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(
                new Uri(imgPath));
            backgroundBrush.ImageSource = image.Source;
            #endregion

            #region Geting Background Path for Buttons

            string buttonPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName + "/Images", "gomb.png");
            ImageBrush buttonBrush = new ImageBrush();
            Image image2 = new Image();
            image2.Source = new BitmapImage(
                new Uri(buttonPath));
            buttonBrush.ImageSource = image2.Source;

            #endregion

            InitializeComponent();

            #region Setting up Backgrounds
            
            mygrid.Background = backgroundBrush;
            first.Background = buttonBrush;
            second.Background = buttonBrush;
            third.Background = buttonBrush;
            fourth.Background = buttonBrush;
            fifth.Background = buttonBrush;
            sixth.Background = buttonBrush;
            seventh.Background = buttonBrush;
            
            
            #endregion
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int stage = int.Parse((e.Source as Button).Tag.ToString()) ;
            Window start=new BeforeFightMenu.MainWindow(stage);
            //(start.DataContext as DungeonWindowViewModel).SetStage(stage);
            start.ShowDialog();
        }

        
    }
}
