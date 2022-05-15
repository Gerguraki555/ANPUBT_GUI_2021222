using FishyRaidFightSystem.Model.Spells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        public void EnemyLoad(string melyikpalya)
        {
            if (melyikpalya == "1")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "sponge.png");

                this.FishesInFight.Add(new Fish() { Elet = 60, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                this.FishesInFight.Add(new Fish() { Elet = 60, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                this.FishesInFight.Add(new Fish() { Elet = 60, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in this.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "2")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "polip.png");

                this.FishesInFight.Add(new Fish() { Elet = 8, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");
                this.FishesInFight.Add(new Fish() { Elet = 6, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");
                this.FishesInFight.Add(new Fish() { Elet = 6, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in this.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "3")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");

                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in this.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "4")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");

                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in this.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "5")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");

                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in this.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "6")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");

                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in this.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "7")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");

                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                this.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in this.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
        }
    }
}
