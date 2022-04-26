using FishyRaidFightSystem.Model;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            teamfishes.Add(selectedFish);
            messenger.Send("Fish Added","TeamInfo");
        }

        public void RemoveFromTeam(Fish selectedFish)
        {
            teamfishes.Remove(selectedFish);
            messenger.Send("Fish Removed","TeamInfo");
        }

        public void Setup(IList<Fish> allfish, List<Fish> teamfishes)
        {
            this.allfish = allfish;
            this.teamfishes = teamfishes;
        }
    }
}
