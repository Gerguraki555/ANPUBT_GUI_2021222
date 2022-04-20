using FishyRaidFightSystem.Logic;
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
        32,
        Brushes.Black);
                drawingContext.DrawText(formattedText, new Point(Area.Width/2-100,0));

                foreach (var item in model.PlayerFish)
                {
                    if (item.sorszam == 1)
                    {
                        item.poziciokezel();
                        if (!item.tamad)
                        {
                            item.x = (double)Area.Width / 4 - 100;
                            item.y = (double)Area.Height / 2 - 100;
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
                            drawingContext.DrawRectangle(Brushes.Red, null, new Rect(Area.Width / 1.159 - 100, Area.Height / 1.3 - 100 + item.pozicio, item.Elet * 1.5, 10));
                        }
                       
                        drawingContext.DrawRectangle(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", item.Eleresiut), UriKind.RelativeOrAbsolute))), null, new Rect(item.x, item.y + item.pozicio, 200, 200));
                    }
                }
            }
            
        }
    }
}
