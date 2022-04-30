using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FishyRaidFightSystem.Model.Spells
{
    public class GainEnergy : Spell
    {
        AudioFileReader effect;
        WaveOut effectout;

        public GainEnergy(Fish hal)
        {
            this.Energiakoltseg = 0;
            this.effect = new AudioFileReader("holy.wav");
            effectout = new WaveOut();
            effectout.Init(effect);
            this.KorszamotNovelo = false;
            this.message = "GAINED 1 ENERGY";
            this.Hala = hal;
            this.mutat = false;
            this.Nev = "GAIN ENERGY";
        }

        public override void Buff(Fish mit, Player jatekos)
        {

            Task ta = new Task(() =>
            {

                effect.CurrentTime = new TimeSpan(0L);
                effectout.Play();
                mit.Eleresiut = "hpholy.png";
                Thread.Sleep(200);
                mit.Eleresiut = mit.regieleres;
                Thread.Sleep(200);
                mit.Eleresiut = "hpholy.png";
                Thread.Sleep(200);

                // hal.Eleresiutatcserel(regi);



            }, TaskCreationOptions.LongRunning);
            ta.Start();

            Task csere = new Task(() =>
            {
                ta.Wait();
                Thread.Sleep(80);
                // hal.Eleresiut = regi;
                if (!mit.meghalt)
                {
                    mit.Eleresiut = mit.regieleres;
                }
                else
                {
                    mit.Eleresiut = mit.dead;
                }

                jatekos.Energy += 1;
                Thread.Sleep(2000);
                effectout.Stop();

            }, TaskCreationOptions.LongRunning);
            csere.Start();
        }

        public override void SzovegLeszed()
        {

            this.mutatasleszedve = true;
            DispatcherTimer timer = new DispatcherTimer();

            timer.Tick += delegate
            {
                this.mutat = false;
                this.mutatasleszedve = false;
                timer.Stop();
            };
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Start();

        }



        public override void Tamad(Fish mit, List<Fish> halak)
        {

        }
    }
}
