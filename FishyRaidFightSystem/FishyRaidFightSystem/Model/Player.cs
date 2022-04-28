using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyRaidFightSystem.Model
{
    public class Player
    {
        public IList<Fish> FishesInFight { get; set; }
        public IList<Fish> AllFishes { get; set; }
        public int Level { get; set; }
        public List<Potion> Potions { get; set; }
        public int SeaCoin { get; set; }

        public Player()
        {
            FishesInFight = new List<Fish>();
            AllFishes = new List<Fish>();
            this.Potions = new List<Potion>();

            FishesInFight.Add(new Fish() { Elet = 100, sorszam = 1, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 0, Kozelsebzes = 10, Helye = 1 });
            FishesInFight.Add(new Fish() { Elet = 100, sorszam = 2, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 30, Kozelsebzes = 10, Helye = 2 });
            FishesInFight.Add(new Fish() { Elet = 100, sorszam = 3, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 20, Kozelsebzes = 10, Helye = 3 });
            AllFishes.Add(new Fish() { Elet = 100, sorszam = 1, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 0, Kozelsebzes = 10, Helye = 1 });
            AllFishes.Add(new Fish() { Elet = 100, sorszam = 2, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio = 30, Kozelsebzes = 10, Helye = 2 });
            SaveAndReadPlayer.Save(this);
        }

    }
}
