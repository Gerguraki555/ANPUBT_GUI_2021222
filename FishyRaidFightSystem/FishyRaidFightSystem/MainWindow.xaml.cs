using FishyRaidFightSystem.Logic;
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
using System.Windows.Threading;
using static FishyRaidFightSystem.Logic.GameLogic;

namespace FishyRaidFightSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameLogic logic;

        public MainWindow()
        {
            InitializeComponent();
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer("music.wav");
            //player.PlayLooping();
          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new GameLogic();
            Display.SetupModel(logic);
            DispatcherTimer dt = new DispatcherTimer(); //Időzítő
            dt.Interval = TimeSpan.FromMilliseconds(5);
            dt.Tick += Dt_Tick; //Így kell időzítőt kezelni
            dt.Start();
            Display.SetupSizes(new Size((int)Grid.ActualWidth, (int)Grid.ActualHeight));
            logic.SetupSizes(new Size((int)Grid.ActualWidth, (int)Grid.ActualHeight));
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (logic != null)
            {
                Display.SetupSizes(new Size((int)Grid.ActualWidth, (int)Grid.ActualHeight));
                logic.SetupSizes(new Size((int)Grid.ActualWidth, (int)Grid.ActualHeight));
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D1)
            {
                logic.Control(Controls.First);
            }
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            logic.TimeStep();
        }
    }
}
