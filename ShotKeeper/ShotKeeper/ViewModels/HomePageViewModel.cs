using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ShotKeeper.Models;
using System;
using System.Collections.ObjectModel;

namespace ShotKeeper.ViewModels
{
    public class HomePageViewModel : BindableBase, INavigationAware
    {
        readonly INavigationService _navigationService;

        public DelegateCommand AddCommand { get; private set; }

        private MainPageListItem selectdGame;
        public MainPageListItem SelectedGame
        {
            get { return selectdGame; }
            set {
                SetProperty(ref selectdGame, value);

                if (null != value)
                {
                    GameSelected(value);
                }
            }
        }

        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GameListItems = new ObservableCollection<MainPageListItem>();

            AddCommand = new DelegateCommand(OnAddCommand, CanAddCommand);
        }

        private bool CanAddCommand()
        {
            return true;
        }

        private async void OnAddCommand()
        {
            await _navigationService.NavigateAsync("ShotKeeperPage");
        }

        public ObservableCollection<MainPageListItem> GameListItems { get; set; }

        private async void GameSelected(MainPageListItem item)
        {
            if (null != item)
            {
                NavigationParameters p = new NavigationParameters();
                p.Add("ID", item.ID);
                await _navigationService.NavigateAsync("ShotKeeperPage", p);
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("ShootingSession"))
            {
                GameListItems.Add(new MainPageListItem { Title = string.Format("Game {0}", GameListItems.Count), LastModified = DateTime.Now });
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
