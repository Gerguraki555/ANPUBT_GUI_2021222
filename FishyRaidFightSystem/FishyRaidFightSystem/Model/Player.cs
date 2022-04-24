using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyRaidFightSystem.Model
{
    public class Player:ObservableRecipient
    {
        public ObservableCollection<Fish> FishesInFight { get; set; }
        public ObservableCollection<Fish> AllFishes { get; set; }
        public int Level { get; set; }
        public ObservableCollection<Potion> Potions { get; set; }
        public int SeaCoin { get; set; }

        public Player()
        {
            FishesInFight = new ObservableCollection<Fish>();
            AllFishes = new ObservableCollection<Fish>();
            this.Potions = new ObservableCollection<Potion>();

            FishesInFight.Add(new Fish() { Elet = 100, sorszam = 1, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 0, Kozelsebzes = 10, Helye = 1 });
            FishesInFight.Add(new Fish() { Elet = 100, sorszam = 2, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 30, Kozelsebzes = 10, Helye = 2 });
            FishesInFight.Add(new Fish() { Elet = 100, sorszam = 3, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 20, Kozelsebzes = 10, Helye = 3 });

            
        }

    }
}
