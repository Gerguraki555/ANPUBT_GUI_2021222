using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishyRaidFightSystem.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BeforeFightMenu
{
    public class DungeonWindowViewModel:ObservableRecipient
    {
        public ObservableCollection<Fish> FishesToBattle { get; set; }
        public ObservableCollection<Fish> Enemies { get; set; }
        public ObservableCollection<Potion> Potions { get; set; }
        public int Stage { get; set; }

        public DungeonWindowViewModel()
        {
            string curretD = Directory.GetCurrentDirectory();
            Player p = (Player)SaveAndReadPlayer.Read(typeof(Player),curretD);

            FishesToBattle = new ObservableCollection<Fish>();
            Enemies = new ObservableCollection<Fish>();
            Potions = new ObservableCollection<Potion>();

            FishesToBattle = p.FishesInFight;
            Potions = p.Potions;



        }
        

    }
}
