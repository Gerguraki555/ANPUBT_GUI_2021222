﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FishyRaidFightSystem;
using FishyRaidFightSystem.Model;


namespace Fishy_Raid
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

        

        private void Open_New_Arena(object sender, RoutedEventArgs e)
        {
            Window win = new FishyRaidFightSystem.MainWindow();
            win.ShowDialog();
        }

        private void Open_Sea_Dungeon_Map(object sender, RoutedEventArgs e)
        {
           // Window sea = new DungeonMap.Mainwindow();
        }
    }
}
