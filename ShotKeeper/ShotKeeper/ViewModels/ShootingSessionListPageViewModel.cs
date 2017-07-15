using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ShotKeeper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShotKeeper.ViewModels
{
    public class ShootingSessionListPageViewModel : BindableBase, INavigationAware
    {
        #region Private Members

        readonly INavigationService _navigationService;
        private ShootingSession _selectedShootingSession;

        #endregion

        #region Constructors

        public ShootingSessionListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ShootingSessionList = new ObservableCollection<ShootingSession>();

            NewCommand = new DelegateCommand(OnNewCommand, CanNewCommand);
        }

        #endregion

        #region Properties

        public ObservableCollection<ShootingSession> ShootingSessionList { get; set; }
        public ShootingSession SelectedShootingSession
        {
            get { return _selectedShootingSession;  }
            set {
                SetProperty(ref _selectedShootingSession, value);

                if (null != value)
                {
                    OnShootingSessionSelected(value);
                }
            }
        }
            
        public DelegateCommand NewCommand { get; private set; }

        #endregion

        #region Methods

        private bool CanNewCommand()
        {
            return true;
        }

        #endregion

        #region Event Handlers

        private async void OnNewCommand()
        {
            await _navigationService.NavigateAsync("ShootingSessionPage");
        }

        private void OnDelete()
        {

        }

        private async void OnShootingSessionSelected(ShootingSession sesh)
        {
            NavigationParameters param = new NavigationParameters();
            param.Add("ShootingSession", sesh);
            await _navigationService.NavigateAsync("ShootingSessionPage", param);
        }

        #endregion

        #region Navigation

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            ShootingSessionList.Clear();
            SelectedShootingSession = null;

            List<ShootingSession> shootingSeshss = await App.Database.GetAllItemsForListDisplayAsync();

            if (shootingSeshss != null)
            {
                foreach (var sesh in shootingSeshss)
                {
                    ShootingSessionList.Add(sesh);
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        #endregion
    }
}
