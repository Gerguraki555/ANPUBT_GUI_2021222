using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FishyRaidFightSystem.Model.Spells
{
    public class Trackle : Spell
    {

        public DispatcherTimer dt = new DispatcherTimer(); //Támadásért felel
        public DispatcherTimer vegzo = new DispatcherTimer();
        public bool delegalthozzaadva { get; set; }
        public string fishbonepath { get; set; }
        AudioFileReader punch;
        WaveOut punchout;

        public Trackle(Fish hal)
        {
            this.Hala = hal;
            this.delegalthozzaadva = false;
            string punchpath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Music", "punch.wav");
            this.punch = new AudioFileReader(punchpath);
            punchout = new WaveOut();
            punchout.Init(punch);
            dt.Interval = TimeSpan.FromMilliseconds(10);
            vegzo.Interval = TimeSpan.FromMilliseconds(100);
            fishbonepath = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName + "/Images", "fishbone.png");
        }

        public override void Buff(Fish mit, Player jatekos)
        {
            throw new NotImplementedException();
        }

        public override void SzovegLeszed()
        {
            throw new NotImplementedException();
        }

        public override void Tamad(Fish mit, ObservableCollection<Fish> halak)
        {


            Hala.oldx = Hala.x;
            Hala.oldy = Hala.y;
            Hala.tamad = true;
            Hala.csikmutat = false;
            int mitx = (int)mit.x;
            int mity = (int)mit.y;
            bool sebzett = false;
            Hala.elfoglalt = true;


            if (!delegalthozzaadva)
            {
                dt.Tick += delegate
                {
                    int szamlalo = 0;

                    while (szamlalo < 10)
                    {
                        if (Hala != null)
                        {
                            if (Hala.visszamegy == false)
                            {
                                if (Hala.x != mitx)
                                {
                                    if (Hala.x > mitx)
                                    {
                                        Hala.x--;
                                    }
                                    else if (Hala.x < mitx) { Hala.x++; }
                                }

                                if (Hala.y != mity)
                                {
                                    if (Hala.y > mity)
                                    {
                                        Hala.y--;
                                    }
                                    else if (Hala.y < mity) { Hala.y++; }
                                }
                            }
                            if (Hala.visszamegy == true)
                            {
                                if (!sebzett)
                                {
                                    mit.Elet -= Hala.Kozelsebzes;
                                    punch.CurrentTime = new TimeSpan(0L);
                                    punchout.Play();

                                    if (mit.Elet <= 0)
                                    {
                                        mit.Elet = 0;
                                        mit.meghalt = true;
                                        mit.Eleresiut = fishbonepath;
                                    }
                                    sebzett = true;

                                }

                                if ((int)Hala.x == (int)Hala.oldx && (int)Hala.y == (int)Hala.oldy)
                                {
                                    Hala.tamad = false;
                                    Hala.csikmutat = true;
                                    Hala.visszamegy = false;
                                    dt.Stop();
                                }

                                if (Hala.x != Hala.oldx)
                                {
                                    if (Hala.x > Hala.oldx)
                                    {
                                        Hala.x--;
                                    }
                                    else if (Hala.x < Hala.oldx) { Hala.x++; }
                                }

                                if (Hala.y != Hala.oldy)
                                {
                                    if (Hala.y > Hala.oldy)
                                    {
                                        Hala.y--;
                                    }
                                    else if (Hala.y < Hala.oldy) { Hala.y++; }
                                }
                            }
                            szamlalo++;
                        }
                    }
                };
            }



            if (!delegalthozzaadva)
            {
                vegzo.Tick += delegate
                {

                    if (!Hala.tamad)
                    {
                        Hala.elfoglalt = false;
                        sebzett = false;
                        dt.Stop();
                        vegzo.Stop();
                        delegalthozzaadva = true;
                    }

                };
            }
            dt.Start();
            vegzo.Start();


        }
    }
}
