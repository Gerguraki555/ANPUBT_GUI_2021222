using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyRaidFightSystem.Model
{
    public class Player
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

            //AllFishes.Add(new Fish() { Elet = 100, sorszam = 1, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 0, Kozelsebzes = 10, Helye = 1 });
            //AllFishes.Add(new Fish() { Elet = 150, sorszam = 2, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 30, Kozelsebzes = 10, Helye = 2 });
            //AllFishes.Add(new Fish() { Elet = 130, sorszam = 2, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 30, Kozelsebzes = 10, Helye = 2 });
            //string filePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, "player.json");
            //SaveAndReadPlayer.Save(this, filePath);
        }

    }
}
