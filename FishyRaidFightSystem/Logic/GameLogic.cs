using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using FishyRaidFightSystem.Model;
using System.Threading;
using FishyRaidFightSystem.Model.Spells;
using System.IO;
using System.Collections.ObjectModel;

namespace FishyRaidFightSystem.Logic
{
    internal class GameLogic : IGameModel
    {
        public event EventHandler Changed;
        public Player Jatekos { get; set; }
        public ObservableCollection<Fish> PlayerFish { get; set; }
        public ObservableCollection<Fish> EnemyFish { get; set; }
        public int sorszam { get; set; }
        static Random R = new Random();
        public int Palyaszam { get; set; }
        public int MaxPalyaszam { get; set; }
        public int Korszam { get; set; }
        public string KovetkezoHal { get; set; }
        public string melyikpalya { get; set; } //Ez alapján dől el, hogy milyen enemy jöjjön
        public Enemy Enemy { get; set; }

        public bool Jatekvege { get; set; }
        public bool Nyert { get; set; }
        public string Gamemode { get; set; }

        public enum Controls
        {
            First, Second, Third, Fourth
        }

        public GameLogic()
        {
            this.melyikpalya = "2";

            // this.Jatekos = new Player();
            //  PlayerSave();

            this.Gamemode = "arena";
            //  this.melyikpalya = "2";
            this.Jatekvege = false;
            this.Nyert = false;

            this.Jatekos = PlayerLoad();

            // this.Jatekos = new Player();
            //  PlayerSave();


            this.Enemy = new Enemy();
            this.Korszam = 0;
            this.Palyaszam = 1;
            this.MaxPalyaszam = 1;
            //  this.PlayerFish = new List<Fish>();
            this.EnemyFish = new ObservableCollection<Fish>();
            //  PlayerFish.Add(new Fish() { Elet = 100, sorszam = 1, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio=0, Kozelsebzes=10,Helye=1});
            //  PlayerFish.Add(new Fish() { Elet = 100, sorszam = 2, Eleresiut = "fishmodel.png", regieleres = "fishmodel.png", pozicio=30, Kozelsebzes=10,Helye=2});
            //  PlayerFish.Add(new Fish() { Elet = 100, sorszam = 3, Eleresiut = "fishmodel.png",regieleres = "fishmodel.png", pozicio=20, Kozelsebzes=10,Helye=3});
            //  EnemyFish.Add(new Fish() { Elet = 100, sorszam = 1, Eleresiut = "polip.png", regieleres = "polip.png", pozicio=10, Kozelsebzes=10,Helye=1});
            //  EnemyFish.Add(new Fish() { Elet = 100, sorszam = 2, Eleresiut = "polip.png", regieleres = "polip.png", pozicio=23, Kozelsebzes=10,Helye=2});
            //  EnemyFish.Add(new Fish() { Elet = 100, sorszam = 3, Eleresiut = "polip.png",regieleres = "polip.png", pozicio=30, Kozelsebzes=10,Helye=3});

            this.sorszam = 0;
            if (Jatekos.FishesInFight[0].Tavolsagi != null)
            {
                KovetkezoHal = "1. TRACKLE 2. " + Jatekos.FishesInFight[0].Tavolsagi.Nev;
            }
            else { KovetkezoHal = "1. TRACKLE"; }
            LevelLoad();
        }

        public Player PlayerLoad()
        {
            string filePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, "player.json");
            Player p = (Player)SaveAndReadPlayer.Read(typeof(Player), filePath);
            Player p2 = new Player();
            foreach (var item in p.FishesInFight)
            {
                //Néhány dolog mentéskor sérül a régi objektumokban, így újakat hozok létre, amelyek törlik a hibás tulajdonságokat


                item.Tavolsagi.Hala = item;
                item.Buff.Hala = item;
                Fish fishy = new Fish() { Elet = item.Maxhp, sorszam = item.sorszam, Eleresiut = item.Eleresiut, regieleres = item.regieleres, pozicio = item.pozicio, Kozelsebzes = item.Kozelsebzes, Helye = item.Helye, Tavolsagi = item.Tavolsagi, Buff = item.Buff, Level = item.Level, EXP = item.EXP, Ero = item.Ero, Maxhp = item.Maxhp };
                fishy.Tavolsagi.Hala = fishy;
                fishy.Buff.Hala = fishy;
                p2.FishesInFight.Add(fishy);
            }
            p2.AllFishes = p.AllFishes;
            p2.SeaCoin = p.SeaCoin;
            return p2;
        }

        public void GameoverCheck()
        {
            bool meghaltazenemy = false;
            int halottszam = 0;
            foreach (var item in Enemy.FishesInFight)
            {
                if (item.Eleresiut == item.dead)
                {
                    halottszam++;
                }
            }
            if (halottszam == 3)
            {
                meghaltazenemy = true;
                Nyert = true;
                GameEnd();
            }
            else if (!meghaltazenemy)
            {
                halottszam = 0;
                foreach (var item in Jatekos.FishesInFight)
                {
                    if (item.Eleresiut != item.regieleres)
                    {
                        halottszam++;
                    }
                }
                if (halottszam == 3)
                {
                    Nyert = false;
                    GameEnd();
                }
            }


        }

        public void GameEnd()
        {
            if (Nyert)
            {
                Random R = new Random();
                if (Gamemode == "arena")
                {
                    int max = 1;
                    foreach (var item in Jatekos.FishesInFight)
                    {
                        if (item.Level > max)
                        {
                            max = item.Level;
                        }
                    }
                    int mennyi = R.Next(10, max * 10);
                    AddExp(Jatekos.FishesInFight, mennyi);
                }


                if (Gamemode == "seadungeon")
                {

                    int szam = R.Next(1, 101);
                    if (szam <= 30)
                    {
                        int melyik = 0;
                        string kepe = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Fishes", melyik + ".png");
                        if (melyikpalya == "1")
                        {
                            melyik = R.Next(1, 51);
                            Fish reward = new Fish()
                            {
                                Elet = R.Next(40, 110),
                                Eleresiut = kepe,
                                Ero = R.Next(5, 13),
                                Level = 1,
                                Buff = null,
                                Tavolsagi = null,
                                kepszam = melyik.ToString()
                            };
                            Jatekos.AllFishes.Add(reward);
                            AddExp(Jatekos.FishesInFight, 10);
                        }
                        else if (melyikpalya == "2")
                        {
                            melyik = R.Next(1, 51);
                            Fish reward = new Fish()
                            {
                                Elet = R.Next(60, 130),
                                Eleresiut = kepe,
                                Ero = R.Next(9, 18),
                                Level = 1,
                                Buff = null,
                                Tavolsagi = null,
                                kepszam=melyik.ToString()
                            };
                            int legyentavolsagi = R.Next(1, 3);
                            if (legyentavolsagi != 1)
                            {
                                reward.Tavolsagi = new SmellyBubble(reward);
                            }

                            Jatekos.AllFishes.Add(reward);
                            AddExp(Jatekos.FishesInFight, 20);

                        }
                        else if (melyikpalya == "3")
                        {
                            melyik = R.Next(50, 101);
                            AddExp(Jatekos.FishesInFight, 30);
                        }
                        else if (melyikpalya == "4")
                        {
                            melyik = R.Next(50, 101);
                            AddExp(Jatekos.FishesInFight, 40);
                        }
                        else if (melyikpalya == "5")
                        {
                            melyik = R.Next(50, 101);
                            AddExp(Jatekos.FishesInFight, 50);
                        }
                        else if (melyikpalya == "6")
                        {
                            melyik = R.Next(50, 101);
                            AddExp(Jatekos.FishesInFight, 60);
                        }
                        else if (melyikpalya == "7")
                        {
                            melyik = R.Next(50, 101);
                            AddExp(Jatekos.FishesInFight, 70);
                        }
                    }
                }
                PlayerSave();
                Jatekvege = true;
            }

            /*  Task vegveto = new Task(() =>
              {


                  bool mehet = false;
                  while (!mehet)
                  {
                      bool jo = true;
                      foreach (var item in Enemy.FishesInFight)
                      {
                          if (item.tamad != false)
                          {
                              jo = false;
                          }
                      }
                      foreach (var item in Jatekos.FishesInFight)
                      {
                          if (item.tamad != false)
                          {
                              jo = false;
                          }
                      }

                      if (jo)
                      {
                          mehet = true;
                      }
                  }
                  PlayerSave();
                  Jatekvege = true;
              });
              vegveto.Start();*/



        }

        public void AddExp(IList<Fish> halak, int mennyi)
        {
            foreach (var item in halak)
            {
                if ((item.EXP + mennyi) >= item.Level * 100)
                {
                    item.Level += 1;
                    item.Ero += 10;
                    item.Elet += 30;
                    item.Kozelsebzes += 10;
                    item.EXP = (item.EXP + mennyi) - 100;
                }
                else
                {
                    item.EXP += mennyi;
                }
            }


            /*     foreach (var item in p.AllFishes)
                 {
                     item.Tavolsagi.Hala = item;
                     item.Buff.Hala = item;
                 }
                 return p;*/

        }

        public void PlayerSave()
        {
            string filePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName, "player.json");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            foreach (var item in this.Jatekos.FishesInFight)
            {
                if (item.Tavolsagi != null)
                {
                    item.Tavolsagi.Hala = null;
                }
                if (item.Buff != null)
                {
                    item.Buff.Hala = null;
                }
                item.Elet = item.Maxhp;
            }
            foreach (var item in this.Jatekos.AllFishes)
            {
                if (item.Tavolsagi != null)
                {
                    item.Tavolsagi.Hala = null;
                }
                if (item.Buff != null)
                {
                    item.Buff.Hala = null;
                }
            }

            SaveAndReadPlayer.Save(Jatekos, filePath);
        }

        public void LevelLoad()
        {
            if (melyikpalya == "1")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "sponge.png");

                Enemy.FishesInFight.Add(new Fish() { Elet = 60, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 60, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 60, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in Enemy.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);

                }
            }
            else if (melyikpalya == "2")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "polip.png");

                Enemy.FishesInFight.Add(new Fish() { Elet = 8, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");
                Enemy.FishesInFight.Add(new Fish() { Elet = 6, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");
                Enemy.FishesInFight.Add(new Fish() { Elet = 6, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in Enemy.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "3")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");

                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in Enemy.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "4")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");

                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in Enemy.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "5")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");

                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in Enemy.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "6")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");

                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in Enemy.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }
            else if (melyikpalya == "7")
            {
                string imgPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "jellyfish.png");

                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 1, Eleresiut = imgPath, regieleres = imgPath, pozicio = 10, Kozelsebzes = 10, Helye = 1 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 2, Eleresiut = imgPath, regieleres = imgPath, pozicio = 23, Kozelsebzes = 10, Helye = 2 });
                Enemy.FishesInFight.Add(new Fish() { Elet = 80, sorszam = 3, Eleresiut = imgPath, regieleres = imgPath, pozicio = 30, Kozelsebzes = 10, Helye = 3 });
                foreach (var item in Enemy.FishesInFight)
                {
                    item.Tavolsagi = new DoubleBubble(item);
                }
            }

        }

        System.Windows.Size Area; //Méretezi a hátteret

        public void SetupSizes(System.Windows.Size area)
        {
            this.Area = area;

        }

        public void TimeStep()
        {

            foreach (var hal in Jatekos.FishesInFight)
            {
                Rect fishrect = new Rect(hal.x, hal.y, 100, 100);
                Rect fishlovedekrect = new Rect(hal.lovedeke.x, hal.lovedeke.y, 100, 100);

                foreach (var enemy in Enemy.FishesInFight)
                {
                    Rect enemyrect = new Rect(enemy.x, enemy.y, 100, 100);
                    Rect enemylovedekrect = new Rect(enemy.lovedeke.x, enemy.lovedeke.y, 100, 100);
                    if (fishrect.IntersectsWith(enemyrect))
                    {
                        bool teljesult = false;
                        if (hal.tamad)
                        {
                            hal.visszamegy = true;
                            teljesult = true;
                            string regi = enemy.Eleresiut;
                            Task t = new Task(() =>
                            {
                                string hitpath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "punch.png");
                                // enemy.Eleresiut = "punch.png";
                                enemy.Eleresiut = hitpath;
                                Thread.Sleep(50);

                            });

                            t.Start();

                            Task csere = new Task(() =>
                            {
                                t.Wait();
                                Thread.Sleep(80);
                                //     enemy.Eleresiut = regi;
                                if (!enemy.meghalt)
                                {
                                    enemy.Eleresiut = enemy.regieleres;
                                }
                                else
                                {
                                    enemy.Eleresiut = enemy.dead;
                                }


                            });
                            csere.Start();

                        }
                        else if (enemy.tamad && !teljesult)
                        {
                            enemy.visszamegy = true;
                            string regi = hal.Eleresiut;
                            Task t = new Task(() =>
                            {


                                hal.Eleresiut = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "punch.png");
                                Thread.Sleep(50);
                                // hal.Eleresiutatcserel(regi);



                            }, TaskCreationOptions.LongRunning);
                            t.Start();

                            Task csere = new Task(() =>
                            {
                                t.Wait();
                                Thread.Sleep(80);
                                // hal.Eleresiut = regi;
                                if (!hal.meghalt)
                                {
                                    hal.Eleresiut = hal.regieleres;
                                }
                                else
                                {
                                    hal.Eleresiut = hal.dead;
                                }

                            }, TaskCreationOptions.LongRunning);
                            csere.Start();
                        }

                    }

                    /*    if (fishlovedekrect.IntersectsWith(enemyrect)&&enemy.tamad==true)   //Itt lehet, hogy gond lesz
                        {
                            hal.tamad = false;
                            hal.lovedeke.aktiv = false;
                        }
                        else if (enemylovedekrect.IntersectsWith(fishrect) && hal.tamad == true)
                        {
                            enemy.tamad = false;
                            enemy.lovedeke.aktiv = false;
                        }*/

                    if (fishlovedekrect.IntersectsWith(enemyrect) && hal.lovedeke.x != hal.x && hal.lovedeke.y != hal.x)   //Itt lehet, hogy gond lesz
                    {
                        // hal.tamad = false;
                        hal.lovedeke.aktiv = false;
                    }
                    else if (enemylovedekrect.IntersectsWith(fishrect) && enemy.lovedeke.x != enemy.x && enemy.lovedeke.y != enemy.x)
                    {
                        // enemy.tamad = false;
                        enemy.lovedeke.aktiv = false;
                    }

                }
            }

            if ((Korszam == 1 || Korszam == 3 || Korszam == 5) && Jatekvege == false) //Ellenséges halak támadási logikája
            {
                int melyik = 0; //Itt kell beállítani a textet
                if (Jatekos.FishesInFight[0].Tavolsagi != null)
                {
                    KovetkezoHal = "1. TRACKLE 2. " + Jatekos.FishesInFight[0].Tavolsagi.Nev;
                }
                else { KovetkezoHal = "1. TRACKLE"; }
                if (Korszam == 3)
                {
                    melyik = 1;
                    if (Jatekos.FishesInFight[1].Tavolsagi != null)
                    {
                        KovetkezoHal = "1. TRACKLE 2. " + Jatekos.FishesInFight[1].Tavolsagi.Nev;
                    }
                    else { KovetkezoHal = "1. TRACKLE"; }
                }
                else if (Korszam == 5)
                {
                    melyik = 2;
                    if (Jatekos.FishesInFight[2].Tavolsagi != null)
                    {
                        KovetkezoHal = "1. TRACKLE 2. " + Jatekos.FishesInFight[2].Tavolsagi.Nev;
                    }
                    else { KovetkezoHal = "1. TRACKLE"; }
                }

                if (Enemy.FishesInFight[melyik].Eleresiut == Enemy.FishesInFight[melyik].dead)
                {
                    if (melyik + 1 < Enemy.FishesInFight.Count)
                    {
                        if (!(Enemy.FishesInFight[melyik + 1].Eleresiut == Enemy.FishesInFight[melyik + 1].dead))
                        {
                            melyik += 1;
                        }
                    }
                    else if (melyik - 1 >= 0)
                    {
                        if (!(Enemy.FishesInFight[melyik - 1].Eleresiut == Enemy.FishesInFight[melyik - 1].dead))
                        {
                            melyik -= 1;
                        }
                    }
                    else
                    {
                        //Pálya vége
                    }
                }

                if (Enemy.FishesInFight[melyik].Elet <= 0)
                {
                    Enemy.FishesInFight[melyik].meghalt = true;
                }

                bool tamade = false;
                foreach (var item in Enemy.FishesInFight)
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
                    foreach (var item in Jatekos.FishesInFight)
                    {
                        if (vantarsa == false && item.Helye == Enemy.FishesInFight[melyik].Helye && item.meghalt == false)
                        {
                            vantarsa = true;
                            melyiket = item.Helye - 1;
                        }
                        else if (item.meghalt == false && vantarsa == false)
                        {
                            melyiket = item.Helye - 1;
                        }

                    }

                    Task tamad = new Task(() => //Ez a baj
                    {
                        if (Enemy.FishesInFight[melyik].meghalt == false && Enemy.FishesInFight[melyik].Eleresiut != System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "fishbone.png"))
                        {
                            Enemy.FishesInFight[melyik].tamad = true;
                            // Thread.Sleep(5500);
                            bool mehet = false;

                            while (!mehet)
                            {
                                int szabadok = 0;
                                foreach (var item in Jatekos.FishesInFight)
                                {
                                    if (!item.elfoglalt)
                                    {
                                        szabadok++;
                                    }
                                }
                                if (szabadok == 3)
                                {
                                    //   Thread.Sleep(2000);
                                    mehet = true;
                                }
                                else { Thread.Sleep(700); }
                            }

                            if (Jatekos.FishesInFight[melyiket].meghalt == false)
                            {
                                if ((Enemy.FishesInFight[melyik].Eleresiut == Enemy.FishesInFight[melyik].dead))
                                {
                                    Enemy.FishesInFight[melyik].tamad = false;

                                    if (melyik + 1 < Enemy.FishesInFight.Count)
                                    {
                                        if (!(Enemy.FishesInFight[melyik + 1].Eleresiut == Enemy.FishesInFight[melyik + 1].dead))
                                        {
                                            melyik += 1;
                                        }
                                    }
                                    else if (melyik - 1 >= 0)
                                    {
                                        if (!(Enemy.FishesInFight[melyik - 1].Eleresiut == Enemy.FishesInFight[melyik - 1].dead))
                                        {
                                            melyik -= 1;
                                        }
                                    }
                                }

                                if (!Jatekvege)
                                {
                                    Enemy.FishesInFight[melyik].Tamad(Jatekos.FishesInFight[melyiket]);
                                }
                            }
                            else
                            {
                                if (melyiket + 1 < Jatekos.FishesInFight.Count)
                                {
                                    if (Jatekos.FishesInFight[melyiket + 1].meghalt == false)
                                    {

                                        if ((Enemy.FishesInFight[melyik].Eleresiut == Enemy.FishesInFight[melyik].dead))
                                        {
                                            Enemy.FishesInFight[melyik].tamad = false;

                                            if (melyik + 1 < Enemy.FishesInFight.Count)
                                            {
                                                if (!(Enemy.FishesInFight[melyik + 1].Eleresiut == Enemy.FishesInFight[melyik + 1].dead))
                                                {
                                                    melyik += 1;
                                                }
                                            }
                                            else if (melyik - 1 >= 0)
                                            {
                                                if (!(Enemy.FishesInFight[melyik - 1].Eleresiut == Enemy.FishesInFight[melyik - 1].dead))
                                                {
                                                    melyik -= 1;
                                                }
                                            }
                                        }

                                        if (!Jatekvege)
                                        {
                                            Enemy.FishesInFight[melyik].Tamad(Jatekos.FishesInFight[melyiket + 1]);
                                        }
                                    }

                                }
                                else if (melyiket - 1 >= 0)
                                {
                                    if (Jatekos.FishesInFight[melyiket - 1].meghalt == false)
                                    {
                                        if ((Enemy.FishesInFight[melyik].Eleresiut == Enemy.FishesInFight[melyik].dead))
                                        {
                                            Enemy.FishesInFight[melyik].tamad = false;

                                            if (melyik + 1 < Enemy.FishesInFight.Count)
                                            {
                                                if (!(Enemy.FishesInFight[melyik + 1].Eleresiut == Enemy.FishesInFight[melyik + 1].dead))
                                                {
                                                    melyik += 1;
                                                }
                                            }
                                            else if (melyik - 1 >= 0)
                                            {
                                                if (!(Enemy.FishesInFight[melyik - 1].Eleresiut == Enemy.FishesInFight[melyik - 1].dead))
                                                {
                                                    melyik -= 1;
                                                }
                                            }
                                        }

                                        if (!Jatekvege)
                                        {
                                            Enemy.FishesInFight[melyik].Tamad(Jatekos.FishesInFight[melyiket - 1]);
                                        }
                                    }
                                }
                            }

                        }

                    });

                    Task tavolsagi = new Task(() =>
                    {
                        if (Enemy.FishesInFight[melyik].meghalt == false && Enemy.FishesInFight[melyik].Eleresiut != System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "fishbone.png"))
                        {
                            Enemy.FishesInFight[melyik].tamad = true;
                            // Thread.Sleep(5500);
                            bool mehet = false;

                            while (!mehet)
                            {
                                int szabadok = 0;
                                foreach (var item in Jatekos.FishesInFight)
                                {
                                    if (!item.elfoglalt)
                                    {
                                        szabadok++;
                                    }
                                }
                                if (szabadok == 3)
                                {
                                    Thread.Sleep(2000);
                                    mehet = true;
                                }
                                else { Thread.Sleep(700); }
                            }

                            if (Jatekos.FishesInFight[melyiket].meghalt == false)
                            {
                                if ((Enemy.FishesInFight[melyik].Eleresiut == Enemy.FishesInFight[melyik].dead))
                                {
                                    Enemy.FishesInFight[melyik].tamad = false;

                                    if (melyik + 1 < Enemy.FishesInFight.Count)
                                    {
                                        if (!(Enemy.FishesInFight[melyik + 1].Eleresiut == Enemy.FishesInFight[melyik + 1].dead))
                                        {
                                            melyik += 1;
                                        }
                                    }
                                    else if (melyik - 1 >= 0)
                                    {
                                        if (!(Enemy.FishesInFight[melyik - 1].Eleresiut == Enemy.FishesInFight[melyik - 1].dead))
                                        {
                                            melyik -= 1;
                                        }
                                    }
                                }
                                if (!Jatekvege)
                                {
                                    Enemy.FishesInFight[melyik].Tavolsagi.Tamad(Jatekos.FishesInFight[melyiket], Jatekos.FishesInFight);
                                }


                            }
                            else
                            {
                                if (melyiket + 1 < Jatekos.FishesInFight.Count)
                                {
                                    if (Jatekos.FishesInFight[melyiket + 1].meghalt == false)
                                    {

                                        if ((Enemy.FishesInFight[melyik].Eleresiut == Enemy.FishesInFight[melyik].dead))
                                        {
                                            Enemy.FishesInFight[melyik].tamad = false;

                                            if (melyik + 1 < Enemy.FishesInFight.Count)
                                            {
                                                if (!(Enemy.FishesInFight[melyik + 1].Eleresiut == Enemy.FishesInFight[melyik + 1].dead))
                                                {
                                                    melyik += 1;
                                                }
                                            }
                                            else if (melyik - 1 >= 0)
                                            {
                                                if (!(Enemy.FishesInFight[melyik - 1].Eleresiut == Enemy.FishesInFight[melyik - 1].dead))
                                                {
                                                    melyik -= 1;
                                                }
                                            }
                                        }

                                        Enemy.FishesInFight[melyik].Tavolsagi.Tamad(Jatekos.FishesInFight[melyiket + 1], Jatekos.FishesInFight);
                                    }

                                }
                                else if (melyiket - 1 >= 0)
                                {
                                    if (Jatekos.FishesInFight[melyiket - 1].meghalt == false)
                                    {

                                        if ((Enemy.FishesInFight[melyik].Eleresiut == Enemy.FishesInFight[melyik].dead))
                                        {
                                            Enemy.FishesInFight[melyik].tamad = false;

                                            if (melyik + 1 < Enemy.FishesInFight.Count)
                                            {
                                                if (!(Enemy.FishesInFight[melyik + 1].Eleresiut == Enemy.FishesInFight[melyik + 1].dead))
                                                {
                                                    melyik += 1;
                                                }
                                            }
                                            else if (melyik - 1 >= 0)
                                            {
                                                if (!(Enemy.FishesInFight[melyik - 1].Eleresiut == Enemy.FishesInFight[melyik - 1].dead))
                                                {
                                                    melyik -= 1;
                                                }
                                            }
                                        }

                                        if (!Jatekvege)
                                        {
                                            Enemy.FishesInFight[melyik].Tavolsagi.Tamad(Jatekos.FishesInFight[melyiket - 1], Jatekos.FishesInFight);
                                        }
                                    }
                                }
                            }

                        }

                    });

                    if (Enemy.FishesInFight[melyik].meghalt == false)
                    {
                        bool mehet = false;

                        while (!mehet)
                        {
                            int szam = R.Next(0, 2);
                            if (szam == 0)
                            {
                                tamad.Start();
                                mehet = true;
                            }
                            else if (szam == 1 && Enemy.Energy >= Enemy.FishesInFight[melyik].Tavolsagi.Energiakoltseg)
                            {
                                tavolsagi.Start();
                                Task varo = new Task(() =>
                                {
                                    tavolsagi.Wait();
                                    Enemy.FishesInFight[melyik].tamad = false;
                                    Enemy.Energy -= Enemy.FishesInFight[melyik].Tavolsagi.Energiakoltseg;
                                });
                                varo.Start();
                                mehet = true;
                            }
                        }
                    }
                    if (Korszam == 5)
                    {
                        Korszam = 0;
                    }
                    else { Korszam++; }



                    Task Novelo = new Task(() =>
                    {
                        while (Enemy.FishesInFight[melyik].tamad == true)
                        {
                            Thread.Sleep(300);
                        }
                        Jatekos.Energy += 1;
                    });
                    Novelo.Start();



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
                    foreach (var item in Jatekos.FishesInFight)
                    {
                        if (item.tamad)
                        {
                            vanetamado = true;
                        }
                    }
                    foreach (var item in Enemy.FishesInFight)
                    {
                        if (item.tamad)
                        {
                            vanetamado = true;
                        }
                    }
                    if (Korszam == 0 && !vanetamado || Korszam == 2 && !vanetamado || Korszam == 4 && !vanetamado)
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
                        foreach (var item in Enemy.FishesInFight)
                        {
                            if (vantarsa == false && item.Helye == Jatekos.FishesInFight[hely].Helye && item.meghalt == false)
                            {
                                vantarsa = true;
                                melyiket = item.Helye - 1;
                            }
                            else if (item.meghalt == false && vantarsa == false)
                            {
                                melyiket = item.Helye - 1;
                            }


                        }
                        if (Jatekos.FishesInFight[hely].meghalt == false)
                        {
                            Jatekos.FishesInFight[hely].Tamad(Enemy.FishesInFight[melyiket]); //Itt még állítani kell
                        }
                        Enemy.Energy++;
                        Korszam++;
                        Jatekos.FishesInFight[hely].Buff.Befejezett = false;


                    }

                    break;
                case Controls.Second:

                    vanetamado = false;
                    foreach (var item in Jatekos.FishesInFight)
                    {
                        if (item.tamad)
                        {
                            vanetamado = true;
                        }
                    }
                    foreach (var item in Enemy.FishesInFight)
                    {
                        if (item.tamad)
                        {
                            vanetamado = true;
                        }
                    }
                    if (Korszam == 0 && !vanetamado || Korszam == 2 && !vanetamado || Korszam == 4 && !vanetamado)
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
                        foreach (var item in Enemy.FishesInFight)
                        {
                            if (vantarsa == false && item.Helye == Jatekos.FishesInFight[hely].Helye && item.meghalt == false)
                            {
                                vantarsa = true;
                                melyiket = item.Helye - 1;
                            }
                            else if (item.meghalt == false && vantarsa == false)
                            {
                                melyiket = item.Helye - 1;
                            }


                        }
                        if (Jatekos.FishesInFight[hely].meghalt == false && Jatekos.Energy >= Jatekos.FishesInFight[hely].Tavolsagi.Energiakoltseg)
                        {
                            Jatekos.FishesInFight[hely].lovedeke.aktiv = true;
                            Jatekos.FishesInFight[hely].Tavolsagi.Tamad(Enemy.FishesInFight[melyiket], Enemy.FishesInFight); //Itt még állítani kell
                            Jatekos.Energy -= Jatekos.FishesInFight[hely].Tavolsagi.Energiakoltseg;
                            Enemy.Energy++;
                            Korszam++;
                            Jatekos.FishesInFight[hely].Buff.Befejezett = false;

                        }




                    }


                    break;
                case Controls.Third:

                    vanetamado = false;
                    foreach (var item in Jatekos.FishesInFight)
                    {
                        if (item.tamad)
                        {
                            vanetamado = true;
                        }
                    }
                    foreach (var item in Enemy.FishesInFight)
                    {
                        if (item.tamad)
                        {
                            vanetamado = true;
                        }
                    }

                    if (Korszam == 0 && !vanetamado || Korszam == 2 && !vanetamado || Korszam == 4 && !vanetamado)
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

                        if (!Jatekos.FishesInFight[hely].Buff.Befejezett)
                        {
                            Jatekos.FishesInFight[hely].Buff.Buff(Jatekos.FishesInFight[hely], Jatekos);
                            Jatekos.Energy -= Jatekos.FishesInFight[hely].Buff.Energiakoltseg;
                            Jatekos.FishesInFight[hely].Buff.Befejezett = true;
                            if (Jatekos.FishesInFight[hely].Buff.KorszamotNovelo)
                            {
                                Korszam++;
                            }
                            Jatekos.FishesInFight[hely].Buff.mutat = true;
                        }
                    }

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
