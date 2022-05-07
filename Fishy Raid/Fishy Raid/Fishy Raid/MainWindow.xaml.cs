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
using DungeonMap;
using TeamEditor;

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
        private void Setup()
        {
            //Brush b = new SolidColorBrush();
            
            //Trigger t = new Trigger()
            //{
            //    SourceName = "",
            //    Value=true,
            //    Property=Button.IsMouseOverProperty,
                
            //};
            //arenabutton.
            //Setter s = new Setter()
            //{
            //    Property = Button.IsMouseOverProperty,
            //    Value =Foreground                
                
            //};
        }
            
        private void Open_New_Arena(object sender, RoutedEventArgs e)
        {
            Window win = new FishyRaidFightSystem.MainWindow();
            this.Close();
            win.ShowDialog();            
        }

        private void Open_New_SeaDungeon(object sender, RoutedEventArgs e)
        {
            Window dungeon = new DungeonMap.MainWindow();
            this.Close();
            dungeon.ShowDialog();            
        }

        private void Open_New_Team_Editor(object sender, RoutedEventArgs e) 
        {
            Window editor = new TeamEditor.MainWindow();
            this.Close();
            editor.ShowDialog();
        }
    }
}
