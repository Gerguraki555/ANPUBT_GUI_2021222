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

            FishesInFight.Add(new Fish() { Elet = 100, sorszam = 1, Eleresiut = "polip.png", regieleres = "polip.png", pozicio = 10, Kozelsebzes = 10, Helye = 1 });
            FishesInFight.Add(new Fish() { Elet = 100, sorszam = 2, Eleresiut = "polip.png", regieleres = "polip.png", pozicio = 23, Kozelsebzes = 10, Helye = 2 });
            FishesInFight.Add(new Fish() { Elet = 100, sorszam = 3, Eleresiut = "polip.png", regieleres = "polip.png", pozicio = 30, Kozelsebzes = 10, Helye = 3 });
        }
    }
}
