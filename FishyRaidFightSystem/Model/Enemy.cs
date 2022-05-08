using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyRaidFightSystem.Model
{
    public class Enemy
    {
        public ObservableCollection<Fish> FishesInFight { get; set; }
        public ObservableCollection<Fish> AllFishes { get; set; }
        public int Level { get; set; }
        public ObservableCollection<Potion> Potions { get; set; }
        public int Energy { get; set; }

        public Enemy()
        {
            this.AllFishes = new ObservableCollection<Fish>();
            this.FishesInFight = new ObservableCollection<Fish>();
            this.Energy = 3;

           
        }
    }
}
