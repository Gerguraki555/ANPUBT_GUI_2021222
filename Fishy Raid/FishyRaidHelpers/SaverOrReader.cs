using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyRaidHelpers
{
    public static class SaverOrReader
    {
        public static Player Read()
        {
            Player p = new Player();
            string currentD = Directory.GetCurrentDirectory();
            string read=File.ReadAllText(currentD);

            return p;
        }

        public static void Save(Player p)
        {
            string toSave = p.ToString();
            string currentD = Directory.GetCurrentDirectory();
            File.WriteAllText(currentD,toSave);
        }

    }
}
