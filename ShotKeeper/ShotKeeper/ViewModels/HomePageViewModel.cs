using Prism.Mvvm;
using Prism.Navigation;
using ShotKeeper.Models;
using System;
using System.Collections.ObjectModel;

namespace ShotKeeper.ViewModels
{
    public class HomePageViewModel : BindableBase, INavigationAware
    {
        public HomePageViewModel()
        {
            GameListItems = new ObservableCollection<MainPageListItem>();
            
        }

        public ObservableCollection<MainPageListItem> GameListItems { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            for (int i = 0; i < 20; i++)
            {
                GameListItems.Add(new MainPageListItem { Title = string.Format("Game {0}", i), LastModified = DateTime.Now });
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
