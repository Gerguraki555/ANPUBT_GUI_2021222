using FishyRaidFightSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BeforeFightMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int Stage { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(int stage)
        {
            #region Geting Path for grid background
            this.Stage = stage;
            string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "dungeon.jpg");
            ImageBrush backgroundBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(
                new Uri(imgPath));
            backgroundBrush.ImageSource = image.Source;

            #endregion

            #region Geting Path for button backgroung

            string buttonPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "gomb.png");
            ImageBrush buttonBrush = new ImageBrush();
            Image image2 = new Image();
            image2.Source = new BitmapImage(
                new Uri(buttonPath));
            buttonBrush.ImageSource = image2.Source;

            #endregion

            InitializeComponent();
            
            (this.DataContext as DungeonWindowViewModel).Stage =stage;
           
            myGrid.Background = backgroundBrush;
            view.Background = buttonBrush;
            start.Background = buttonBrush;
            

        }


        private void view_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
           
            Window window = new FishyRaidFightSystem.MainWindow(Stage);
            this.Close();
            window.Show();
        }
    }
}
