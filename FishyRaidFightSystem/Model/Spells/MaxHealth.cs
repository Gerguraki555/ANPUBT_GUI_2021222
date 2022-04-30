﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FishyRaidFightSystem.Model.Spells
{
    public class MaxHealth : Spell
    {
        AudioFileReader effect;
        WaveOut effectout;

        public MaxHealth(Fish hal)
        {
            this.Energiakoltseg = 2;
            this.effect = new AudioFileReader("holy.wav");
            effectout = new WaveOut();
            this.Hala = hal;
            effectout.Init(effect);
            this.KorszamotNovelo = false;
            this.message = "GAINED 1 ENERGY";
            this.mutat = false;
            this.Nev = "STRONG RECOVER";
        }

        public override void Buff(Fish mit, Player jatekos)
        {
            Random R = new Random();
            int szam = R.Next((Hala.Elet / 10) * 3, (Hala.Elet / 5) * 4); //10-20%
            if (Hala.Elet + szam <= Hala.Maxhp)
            {
                Hala.Elet += szam;
                this.message = "YOU HEALED " + szam + " HP";
            }
            else
            {
                Hala.Elet = Hala.Maxhp;
                this.message = "YOUR HP IS MAX";
            }



            Task ta = new Task(() =>
            {

                effect.CurrentTime = new TimeSpan(0L);
                effectout.Play();
                mit.Eleresiut = "holy.png";
                Thread.Sleep(200);
                mit.Eleresiut = mit.regieleres;
                Thread.Sleep(200);
                mit.Eleresiut = "holy.png";
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





                Thread.Sleep(2000);
                effectout.Stop();

            }, TaskCreationOptions.LongRunning);
            csere.Start();
        }

        public override void SzovegLeszed()
        {
            Task t = new Task(() =>
            {
                this.mutatasleszedve = true;
                Thread.Sleep(2000);
                this.mutat = false;
                this.mutatasleszedve = false;

            });
            t.Start();
        }

        public override void Tamad(Fish mit, ObservableCollection<Fish> halak)
        {

        }
    }
}