using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FishyRaidFightSystem.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BeforeFightMenu
{
    public class DungeonWindowViewModel:ObservableRecipient
    {
        public ObservableCollection<Fish> FishesToBattle { get; set; }
        public ObservableCollection<Enemy> Enemies { get; set; }
        public ObservableCollection<Potion> Potions { get; set; }
        public int Stage { get; set; }
        public DungeonWindowViewModel()
        {
           string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, "player.json");
            Player p = (Player)SaveAndReadPlayer.Read(typeof(Player),path);

            FishesToBattle = new ObservableCollection<Fish>();
            Enemies = new ObservableCollection<Enemy>();
            Potions = new ObservableCollection<Potion>();

            FishesToBattle = p.FishesInFight;
            Potions = p.Potions;
            
            Enemies= SetupEnemys();
            ;
        }
        public ObservableCollection<Enemy> SetupEnemys() 
        {
            StreamWriter sw = new StreamWriter("asd.txt");
            ObservableCollection<Enemy> enemys = new ObservableCollection<Enemy>();
            
            return enemys;
        }
        

    }
}
