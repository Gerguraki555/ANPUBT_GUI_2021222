﻿using FishyRaidFightSystem.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FishyRaidFightSystem.Renderer
{
    internal class Display : FrameworkElement
    {
        Size Area; //Háttér méret
        static Random R = new Random();
        IGameModel model;

        public void SetupSizes(Size area)
        {
            this.Area = area;
            this.InvalidateVisual(); //Újrarajzolás
        }

        public void SetupModel(IGameModel model)
        {
            this.model = model;
            this.model.Changed += (sender, eventargs) => this.InvalidateVisual();
        }

        public Brush MapBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "arenakep.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush FishBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "fishmodel.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        [Obsolete]
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (Area.Width > 0 && Area.Height > 0 && model != null)
            {
                drawingContext.DrawRectangle(MapBrush, null, new Rect(0, 0, Area.Width, Area.Height));

                FormattedText formattedText = new FormattedText(
        "Wave "+model.Palyaszam+"/"+model.MaxPalyaszam,
        CultureInfo.GetCultureInfo("en-us"),
        FlowDirection.LeftToRight,
        new Typeface("Verdana"),
        64,
        Brushes.Yellow);
                FormattedText halstatusz = new FormattedText(
        "SPELLS: "+model.KovetkezoHal,
        CultureInfo.GetCultureInfo("en-us"),
        FlowDirection.LeftToRight,
        new Typeface("Verdana"),
        32,
        Brushes.Yellow);
                drawingContext.DrawText(formattedText, new Point(Area.Width/2-200,0));
                drawingContext.DrawText(halstatusz, new Point(Area.Width/7, Area.Height/1.1));

                foreach (var item in model.Jatekos.FishesInFight)
                {
                    if (item.sorszam == 1)
                    {
                        item.poziciokezel();
                        if (!item.tamad)
                        {
                            item.x = (double)Area.Width / 4 - 100;
                            item.y = (double)Area.Height / 2 - 100;
                        }

                        if (item.csikmutat)
                        {
                            
                            if (model.Korszam == 0&&model.Jatekos.FishesInFight[0].meghalt==false && model.EnemyFish[0].tamad == false && model.EnemyFish[1].tamad == false && model.EnemyFish[2].tamad == false)
                            {
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "leftarrow.png"), UriKind.RelativeOrAbsolute))), null, new Rect(item.x + 200, item.y + 100 + item.pozicio, 70, 40));
                            }
                            drawingContext.DrawRectangle(Brushes.Red, null, new Rect(Area.Width / 3.9 - 100, Area.Height / 2 - 100 + item.pozicio, item.Elet * 1.5, 10));
                          
                        }
                        drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", item.Eleresiut), UriKind.RelativeOrAbsolute))), null, new Rect(item.x, item.y + item.pozicio, 200, 200));
                   
                    }
                    else if (item.sorszam == 2)
                    {
                        item.poziciokezel();

                        if (!item.tamad)
                        {
                            item.x = (double)Area.Width / 15 - 100;
                            item.y = (double)Area.Height / 8 - 100;
                        }

                        if (item.csikmutat)
                        {
                            
                            if (model.Korszam == 2 && model.Jatekos.FishesInFight[1].meghalt == false && model.EnemyFish[0].tamad == false && model.EnemyFish[1].tamad == false && model.EnemyFish[2].tamad == false)
                            {
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "leftarrow.png"), UriKind.RelativeOrAbsolute))), null, new Rect(item.x + 200, item.y + 100 + item.pozicio, 70, 40));
                            }
                            drawingContext.DrawRectangle(Brushes.Red, null, new Rect(Area.Width / 14 - 100, Area.Height / 8 - 100 + item.pozicio, item.Elet * 1.5, 10));
                        }
                        
                        drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", item.Eleresiut), UriKind.RelativeOrAbsolute))), null, new Rect(item.x, item.y + item.pozicio, 200, 200));
                    }
                    else if (item.sorszam == 3)
                    {
                        item.poziciokezel();

                        if (!item.tamad)
                        {
                            item.x = (double)Area.Width / 15 - 100;
                            item.y = (double)Area.Height / 1.3 - 100;
                        }

                        if (item.csikmutat)
                        {
                            
                            if (model.Korszam == 4 && model.Jatekos.FishesInFight[2].meghalt == false && model.EnemyFish[0].tamad == false && model.EnemyFish[1].tamad == false && model.EnemyFish[2].tamad == false)
                            {
                                drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "leftarrow.png"), UriKind.RelativeOrAbsolute))), null, new Rect(item.x + 200, item.y + 100 + item.pozicio, 70, 40));
                            }
                            drawingContext.DrawRectangle(Brushes.Red, null, new Rect(Area.Width / 14 - 100, Area.Height / 1.3 - 100 + item.pozicio, item.Elet * 1.5, 10));
                        }
                       
                        drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", item.Eleresiut), UriKind.RelativeOrAbsolute))), null, new Rect(item.x, item.y+ item.pozicio, 200, 200));
                    }
                }
                foreach (var item in model.EnemyFish)
                {
                    if (item.sorszam == 1)
                    {
                        item.poziciokezel();

                        if (!item.tamad)
                        {
                            item.x = (double)Area.Width / 1.4 - 100;
                            item.y = (double)Area.Height / 2 - 100;
                        }

                        if (item.csikmutat)
                        {
                            
                            drawingContext.DrawRectangle(Brushes.Red, null, new Rect(Area.Width / 1.35 - 100, Area.Height / 2 - 100 + item.pozicio, item.Elet * 1.5, 10));
                        }
                        
                        drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", item.Eleresiut), UriKind.RelativeOrAbsolute))), null, new Rect(item.x, item.y + item.pozicio, 200, 200));
                    }
                    else if (item.sorszam == 2)
                    {
                        item.poziciokezel();

                        if (!item.tamad)
                        {
                            item.x = (double)Area.Width / 1.2 - 100;
                            item.y = (double)Area.Height / 8 - 100;
                        }

                        if (item.csikmutat)
                        {
                            
                            drawingContext.DrawRectangle(Brushes.Red, null, new Rect(Area.Width / 1.159 - 100, Area.Height / 8 - 100 + item.pozicio, item.Elet * 1.5, 10));
                        }
                        
                        drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", item.Eleresiut), UriKind.RelativeOrAbsolute))), null, new Rect(item.x, item.y + item.pozicio, 200, 200));
                    }
                    else if (item.sorszam == 3)
                    {
                        item.poziciokezel();

                        if (!item.tamad)
                        {
                            item.x = (double)Area.Width / 1.2 - 100;
                            item.y = (double)Area.Height / 1.3 - 100;
                        }

                        if (item.csikmutat)
                        {
                           
                            drawingContext.DrawRectangle(Brushes.Red, null, new Rect(Area.Width / 1.159 - 100, Area.Height / 1.3 - 100 + item.pozicio, item.Elet * 1.5, 10));
                        }
                       
                        drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", item.Eleresiut), UriKind.RelativeOrAbsolute))), null, new Rect(item.x, item.y + item.pozicio, 200, 200));
                    }
                }
            }
            
        }
    }
}
