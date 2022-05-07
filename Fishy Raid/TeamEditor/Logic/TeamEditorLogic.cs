using FishyRaidFightSystem.Model;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TeamEditor.Logic
{
    public class TeamEditorLogic : ITeamEditorLogic
    {
        IList<Fish> allfish;
        IList<Fish> teamfishes;
        IMessenger messenger;
        public TeamEditorLogic(IMessenger messenger)
        {
            this.messenger = messenger;
        }
        public void AddToTeam(Fish selectedFish)
        {
            if (teamfishes.Count<3)
            {                
                teamfishes.Add(selectedFish);
                messenger.Send("Fish Added", "TeamInfo");
                allfish.Remove(selectedFish);
                messenger.Send("Fish Removed","TeamInfo");
            }
            else
            {
                MessageBox.Show("You can only take 3 Fish to Battle!");
            }            
        }

        

        public void RemoveFromTeam(Fish selectedFish)
        {
            teamfishes.Remove(selectedFish);
            messenger.Send("Fish Removed","TeamInfo");
            allfish.Add(selectedFish);
            messenger.Send("Fish Added", "TeamInfo");
        }
        public void Save(ref Player p) 
        {
            if (teamfishes.Count == 3)
            {
                string filePath= Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, "player.json");
                SaveAndReadPlayer.Save(p,filePath);
                MessageBox.Show("Team Saved!");
            }
            else
            {
                MessageBox.Show("You need to have 3 Fishes in the Team to Save!");
            }

        }
        public void Setup(IList<Fish> allfish, IList<Fish> teamfishes)
        {
            this.allfish = allfish;
            this.teamfishes = teamfishes;
        }
    }
}
