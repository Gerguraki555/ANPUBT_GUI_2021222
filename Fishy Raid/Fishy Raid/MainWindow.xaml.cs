﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FishyRaidFightSystem;

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
           // Menumover.MoveTo(tesztkep, tesztkep.Width + 1, tesztkep.Height + 1);
            
        }

        private void menugrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //stackPanel.Margin = menugrid.ActualHeight / 2;
        }

        private void Open_New_Arena(object sender, RoutedEventArgs e)
        {
            Window win = new FishyRaidFightSystem.MainWindow();
           
            
        }
    }
}
