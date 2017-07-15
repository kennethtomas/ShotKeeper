using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ShotKeeper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShotKeeper.ViewModels
{
    public class SessionsPageViewModel : BindableBase, INavigationAware
    {
        #region Private Members

        readonly INavigationService _navigationService;

        bool _busy;

        private ObservableCollection<ShootingSession> _shootingSessions;
        private ShootingSession _selectedShootingSession;
        private ObservableCollection<Grouping<string, ShootingSession>> _shootingSessionsGrouped;

        #endregion

        #region Constructors

        public SessionsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _shootingSessions = new ObservableCollection<ShootingSession>();
            ShootingSessionsGrouped = new ObservableCollection<Grouping<string, ShootingSession>>();

            RefreshDataCommand = new DelegateCommand(
                async () => await RefreshData());

            AddCommand = new DelegateCommand(OnAdd);
        }

        #endregion

        #region Properties

        public ObservableCollection<ShootingSession> ShootingSessions
        {
            get { return _shootingSessions; }
            set { SetProperty(ref _shootingSessions, value); }
        }

        public ShootingSession SelectedShootingSession
        {
            get { return _selectedShootingSession; }
            set {
                SetProperty(ref _selectedShootingSession, value);
                GoToSessionAsync(value);
            }
        }

        public ObservableCollection<Grouping<string, ShootingSession>> ShootingSessionsGrouped
        {
            get { return _shootingSessionsGrouped; }
            set { SetProperty(ref _shootingSessionsGrouped, value); }
        }
        
        private async Task GoToSessionAsync(ShootingSession shootingSession)
        {
            if (null != shootingSession)
            {
                NavigationParameters param = new NavigationParameters
                {
                    { "ShootingSessions", _shootingSessions },
                    { "ShootingSession", shootingSession }
                };
                await _navigationService.NavigateAsync("ShotKeeperPage", param);
                
            }
        }

        public bool IsBusy
        {
            get { return _busy; }
            set
            {
                _busy = value;
            }
        }

        #endregion

        #region Commands

        #endregion

        public DelegateCommand RefreshDataCommand { get; }
        public DelegateCommand AddCommand { get; }

        private async void OnAdd()
        {
            NavigationParameters param = new NavigationParameters
            {
                { "ShootingSessions", _shootingSessions },
                { "ShootingSession", new ShootingSession() { ID = GetNextSessionID(), CreatedTime = DateTime.Now } }
            };
            await _navigationService.NavigateAsync("ShotKeeperCarouselPage", param);
            
        }

        #region Methods

        private int GetNextSessionID()
        {
            int nextID = 0;

            if (_shootingSessions.Count > 0)
            {
                nextID = _shootingSessions[_shootingSessions.Count - 1].ID + 1;
            }

            return nextID;
        }
        
        async Task RefreshData()
        {
            IsBusy = true;
            //Load Data Here
            await Task.Delay(2000);

            IsBusy = false;
        }


        #endregion

        #region Navigation

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("ShootingSessions"))
            {
                if (parameters.TryGetValue("ShootingSessions", out ObservableCollection<ShootingSession> shootingSesh))
                {
                    _shootingSessions = shootingSesh;
                }
                else
                {
                    _shootingSessions = new ObservableCollection<ShootingSession>();
                }

                var sorted = from item in _shootingSessions
                             orderby item.CreatedTime.ToString("D")
                             group item by item.CreatedTime.ToString("D") into itemGroup
                             select new Grouping<string, ShootingSession>(itemGroup.Key, itemGroup);

                ShootingSessionsGrouped = new ObservableCollection<Grouping<string, ShootingSession>>(sorted);
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
              
        }

        #endregion
    }
}
