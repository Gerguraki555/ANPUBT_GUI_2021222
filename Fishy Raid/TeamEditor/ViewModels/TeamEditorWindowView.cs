using FishyRaidFightSystem.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamEditor.Logic;

namespace TeamEditor.ViewModels
{
    public class TeamEditorWindowView:ObservableRecipient
    {
        public ObservableCollection<Fish> FishesAboutToFight { get; set; }
        public ObservableCollection<Fish> AllFishes { get; set; }
        private Fish selectedFromAllFishes;

        public Fish SelectedFromAllFishes
        {
            get { return selectedFromAllFishes; }
            set {
                SetProperty(ref selectedFromAllFishes,value);
                (AddToTeam as RelayCommand).NotifyCanExecuteChanged();
                (RemoveFromTeam as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        private Fish selectedFromFishesAboutToFight;

        public Fish SelectedFromFishesAboutToFight
        {
            get { return selectedFromFishesAboutToFight; }
            set {
                SetProperty(ref selectedFromFishesAboutToFight,value);
                (RemoveFromTeam as RelayCommand).NotifyCanExecuteChanged();             
            }
        }

        public ITeamEditorLogic logic { get; set; }
        public ICommand AddToTeam { get; set; }
        public ICommand RemoveFromTeam { get; set; }
        private Player Player;
        public TeamEditorWindowView(ITeamEditorLogic logic)
        {
            this.logic = logic;
            AllFishes = new ObservableCollection<Fish>();
            FishesAboutToFight = new ObservableCollection<Fish>();

            
            this.Player = (Player)SaveAndReadPlayer.Read(typeof(Player));
            AllFishes = (ObservableCollection<Fish>)Player.AllFishes;

            AddToTeam = new RelayCommand(
                ()=>logic.AddToTeam(SelectedFromAllFishes),
                ()=>SelectedFromAllFishes!=null
                );
            RemoveFromTeam = new RelayCommand(
                () => logic.RemoveFromTeam(SelectedFromFishesAboutToFight),
                () => SelectedFromFishesAboutToFight != null
                ) ;
        }
        IMessenger.Register<TeamEditorWindowView>();
        public TeamEditorWindowView()
            :this(Ioc.Default.GetService<ITeamEditorLogic>())
        {

        }



    }
}
