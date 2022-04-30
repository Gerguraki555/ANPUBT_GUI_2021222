using FishyRaidFightSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            InitializeComponent();
            (this.DataContext as DungeonWindowViewModel).Stage =stage;
        }
        
    }
}
