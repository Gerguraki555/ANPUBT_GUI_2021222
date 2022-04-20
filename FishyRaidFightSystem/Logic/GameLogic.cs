using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using FishyRaidFightSystem.Model;
using System.Threading;

namespace FishyRaidFightSystem.Logic
{
    internal class GameLogic : IGameModel
    {
        public event EventHandler Changed;

        public List<Fish> PlayerFish { get; set; }
        public List<Fish> EnemyFish { get; set; }
        public int sorszam { get; set; }

        public int Palyaszam { get; set; }
        public int MaxPalyaszam { get; set; }
        public int Korszam { get; set; }

        public enum Controls
        {
            First, Second, Third, Fourth
        }

        public GameLogic()
        {
            this.Korszam = 0;
            this.Palyaszam = 1;
            this.MaxPalyaszam = 1;
            this.PlayerFish = new List<Fish>();
            this.EnemyFish = new List<Fish>();
            PlayerFish.Add(new Fish() { Elet = 100, sorszam = 1, Eleresiut = "fishmodel.png" ,pozicio=0, Kozelsebzes=10,Helye=1});
            PlayerFish.Add(new Fish() { Elet = 100, sorszam = 2, Eleresiut = "fishmodel.png" ,pozicio=30, Kozelsebzes=10,Helye=2});
            PlayerFish.Add(new Fish() { Elet = 100, sorszam = 3, Eleresiut = "fishmodel.png" ,pozicio=20, Kozelsebzes=10,Helye=3});
            EnemyFish.Add(new Fish() { Elet = 100, sorszam = 1, Eleresiut = "polip.png" ,pozicio=10, Kozelsebzes=10,Helye=1});
            EnemyFish.Add(new Fish() { Elet = 100, sorszam = 2, Eleresiut = "polip.png" ,pozicio=23, Kozelsebzes=10,Helye=2});
            EnemyFish.Add(new Fish() { Elet = 100, sorszam = 3, Eleresiut = "polip.png" ,pozicio=30, Kozelsebzes=10,Helye=3});
            this.sorszam = 0;
        }

        Size Area; //Méretezi a hátteret

        public void SetupSizes(Size area)
        {
            this.Area = area;
           
        }

        public void TimeStep()
        {
            foreach (var hal in PlayerFish)
            {
                Rect fishrect = new Rect(hal.x , hal.y , 100, 100);

                foreach (var enemy in EnemyFish)
                {
                    Rect enemyrect = new Rect(enemy.x, enemy.y, 100, 100);
                    if (fishrect.IntersectsWith(enemyrect))
                    {
                        if (hal.tamad)
                        {
                            hal.visszamegy = true;
                        }
                         if (enemy.tamad)
                        {
                            enemy.visszamegy = true;
                        }
                    }
                }
            }

            if (Korszam ==1||Korszam==3||Korszam==5)
            {
                int melyik = 0;
                if (Korszam == 3)
                {
                    melyik = 1;
                }
                else if (Korszam == 5)
                {
                    melyik = 2;
                }

                bool tamade = false;
                foreach (var item in EnemyFish)
                {
                    if (item.tamad)
                    {
                        tamade = true;
                    }
                }
                if (!tamade)
                {
                    int melyiket = 0;
                    bool vantarsa = false;
                    foreach (var item in PlayerFish)
                    {
                        if (vantarsa == false && item.Helye == EnemyFish[melyik].Helye && item.meghalt == false)
                        {
                            vantarsa = true;
                            melyiket = item.Helye-1;
                        }
                        else if (item.meghalt == false && vantarsa == false)
                        {
                            melyiket = item.Helye-1;
                        }
                        
                    }

                    Task tamad = new Task(() =>
                      {
                          if (EnemyFish[melyik].meghalt == false)
                          {
                            //  EnemyFish[melyik].tamad = true;
                              Thread.Sleep(5500);
                              EnemyFish[melyik].Tamad(PlayerFish[melyiket]);
                              
                          }
                          
                      });
                    tamad.Start();
                    if (Korszam == 5)
                    {
                        Korszam = 0;
                    }
                    else { Korszam++; }
                }
            }

            Changed?.Invoke(this, null);
        }

        public void FightEngine()
        {

        }

        public void Control(Controls control)
        {
            switch (control)
            {
                case Controls.First:
                    bool vanetamado = false;
                    foreach (var item in PlayerFish)
                    {
                        if (item.tamad)
                        {
                            vanetamado = true;
                        }
                    }
                    foreach (var item in EnemyFish)
                    {
                        if (item.tamad)
                        {
                            vanetamado = true;
                        }
                    }
                    if (Korszam ==0&&!vanetamado||Korszam==2 && !vanetamado || Korszam==4 && !vanetamado)
                    {
                        int hely = 0;
                        if (Korszam == 2)
                        {
                            hely = 1;
                        }
                        else if (Korszam == 4)
                        {
                            hely = 2;
                        }
                        int melyiket = 0;
                        bool vantarsa = false;
                        foreach (var item in EnemyFish)
                        {
                            if (vantarsa == false && item.Helye == PlayerFish[hely].Helye && item.meghalt == false)
                            {
                                vantarsa = true;
                                melyiket = item.Helye-1;
                            }
                            else if (item.meghalt == false && vantarsa == false)
                            {
                                melyiket = item.Helye-1;
                            }

                            
                        }
                        if (PlayerFish[hely].meghalt == false)
                        {
                            PlayerFish[hely].Tamad(EnemyFish[melyiket]); //Itt még állítani kell
                        }
                        Korszam++;



                    }
                   
                    break;
                case Controls.Second:
                   
                    break;
                case Controls.Third:
                  
                    break;
                case Controls.Fourth:

                    break;
                default:
                    break;
            }
            Changed?.Invoke(this, null); //El kell sütni a frissítés miatt az eseményt

        }
    }
}
