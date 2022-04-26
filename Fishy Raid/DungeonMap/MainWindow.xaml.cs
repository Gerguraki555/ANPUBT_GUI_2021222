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
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window start=new BeforeFightMenu.MainWindow(1);
            start.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window start = new BeforeFightMenu.MainWindow(2);
            start.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window start = new BeforeFightMenu.MainWindow(3);
            start.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window start = new BeforeFightMenu.MainWindow(4);
            start.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Window start = new BeforeFightMenu.MainWindow(5);
            start.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Window start = new BeforeFightMenu.MainWindow(6);
            start.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Window start = new BeforeFightMenu.MainWindow(7);
            start.ShowDialog();
        }
    }
}
