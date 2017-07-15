using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShotKeeper.ViewModels
{
    public class ShootingSessionListPageViewModel : BindableBase, INavigationAware
    {
        readonly INavigationService _navigationService;

        public ShootingSessionListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void OnNew()
        {

        }

        private void OnDelete()
        {

        }

        private async void OnShootingSessionSelected()
        {
            NavigationParameters param = new NavigationParameters();
            //param.Add("ShootingSessions", _shootingSessions);
            //param.Add("ShootingSession", new ShootingSession() { ID = GetNextSessionID(), CreatedTime = DateTime.Now });
            param.Add("Session ID", "1234");
            await _navigationService.NavigateAsync("ShootingSessionPage", param);
        }

        #region Navigation

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            // fetch the data from the db
            // parse the data into the collection to be displayed

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        #endregion
    }
}
