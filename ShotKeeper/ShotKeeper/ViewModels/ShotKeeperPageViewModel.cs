using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ShotKeeper.Interfaces;
using ShotKeeper.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace ShotKeeper.ViewModels
{
    public class ShotKeeperPageViewModel : BindableBase, INavigationAware
    {
        #region Constants

        private const string OUT_PERCENTAGE_STRING_TEMPLATE = "{0}%\n({1}/{2})";

        private const string SPEECH_RATIO_STRING_TEMPLATE = "{0} out of {1} {2}";
        private const string SPEECH_FREE_THROWS_NAME = "Free Throws";
        private const string SPEECH_MID_RANGES_NAME = "Field Goals";
        private const string SPEECH_THREE_POINTERS_NAME = "Three Pointers";

        #endregion

        #region Private Members

        INavigationService _navigationService;

        private string _title;
        private string _valueTotalShooting;

        private string _valueFreeThrow;
        private string _valueMidRange;
        private string _valueThreePointers;

        private double _numberOfFreeThrows;
        private double _numberOfMidRanges;
        private double _numberOfThreePointers;

        private double _numberOfFreeThrowsCounted;
        private double _numberOfMidRangesCounted;
        private double _numberOfThreePointersCounted;

        private bool _speakEnabled;

        private bool _buttonIsEnabled;

        private ObservableCollection<ShootingSession> _shootingSessions;
        private ShootingSession _currentSession;

        #endregion

        #region Commands

        public DelegateCommand<String> AddCommand { get; private set; }
        public DelegateCommand<String> RemoveCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public DelegateCommand ListenCommand { get; private set; }

        #endregion

        #region Constructors

        public ShotKeeperPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NumberOfFreeThrows = 0;
            NumberOfMidRanges = 0;
            NumberOfThreePointers = 0;

            NumberOfFreeThrowsCounted = 0;
            NumberOfMidRangesCounted = 0;
            NumberOfThreePointersCounted = 0;

            SpeakEnabled = false;

            UpdateValueFreeThrow();
            UpdateValueMidRange();
            UpdateValueThreePointers();
            UpdateValueTotalShootingPercentage();

            InitialiseCommands();
        }

        private void InitialiseCommands()
        {
            AddCommand = new DelegateCommand<String>(OnAddCommand, CanAddCommand);
            RemoveCommand = new DelegateCommand<String>(OnRemoveCommand, CanRemoveCommand);
            ListenCommand = new DelegateCommand(OnListenCommand, CanListenCommand);
            SaveCommand = new DelegateCommand(OnSaveCommand, CanSaveCommand);
            CancelCommand = new DelegateCommand(OnCancelCommand, CanCancelCommand);
        }

        private bool CanCancelCommand()
        {
            return true;
        }

        private async void OnCancelCommand()
        {
            await _navigationService.GoBackAsync();
        }

        private bool CanSaveCommand()
        {
            return true;
        }

        private async void OnSaveCommand()
        {
            NavigationParameters param = new NavigationParameters();

            var sesh = _shootingSessions.FirstOrDefault(i => i.ID == CurrentSession.ID);
            if (null != sesh)
            {
                sesh.LastModified = DateTime.Now;   
            }
            else
            {
                CurrentSession.LastModified = DateTime.Now;
                _shootingSessions.Add(CurrentSession);
            }
            
            param.Add("ShootingSessions", _shootingSessions);

            await _navigationService.NavigateAsync("SessionsPage", param);
        }

        #endregion

        #region Properties

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string ValueTotalShooting
        {
            get { return _valueTotalShooting; }
            set { SetProperty(ref _valueTotalShooting, value); }
        }

        public string ValueFreeThrow
        {
            get { return _valueFreeThrow; }
            set { SetProperty(ref _valueFreeThrow, value); }
        }
        public string ValueMidRange
        {
            get { return _valueMidRange; }
            set { SetProperty(ref _valueMidRange, value); }
        }
        public string ValueThreePointers
        {
            get { return _valueThreePointers; }
            set { SetProperty(ref _valueThreePointers, value); }
        }

        public double NumberOfFreeThrows
        {
            get {
                return _numberOfFreeThrows;
            }

            set
            {
                _numberOfFreeThrows = value;
            }
        }
        public double NumberOfMidRanges
        {
            get
            {
                return _numberOfMidRanges;
            }

            set
            {
                _numberOfMidRanges = value;
            }
        }
        public double NumberOfThreePointers
        {
            get
            {
                return _numberOfThreePointers;
            }

            set
            {
                _numberOfThreePointers = value;
            }
        }

        public double NumberOfFreeThrowsCounted
        {
            get
            {
                return _numberOfFreeThrowsCounted;
            }

            set
            {
                _numberOfFreeThrowsCounted = value;
            }
        }
        public double NumberOfMidRangesCounted
        {
            get
            {
                return _numberOfMidRangesCounted;
            }

            set
            {
                _numberOfMidRangesCounted = value;
            }
        }
        public double NumberOfThreePointersCounted
        {
            get
            {
                return _numberOfThreePointersCounted;
            }

            set
            {
                _numberOfThreePointersCounted = value;
            }
        }

        public bool SpeakEnabled
        {
            get { return _speakEnabled; }
            set { SetProperty(ref _speakEnabled, value); }
        }

        public IShotKeeper ShotKeeper { get; set; }

        public bool ButtonIsEnabled
        {
            get { return _buttonIsEnabled; }
            set { SetProperty(ref _buttonIsEnabled, value); }
        }

        public ShootingSession CurrentSession
        {
            get { return _currentSession; }
            set { SetProperty(ref _currentSession, value); }
        }

        #endregion

        #region Methods

        private void AddNumberOfFreeThrow()
        {
            NumberOfFreeThrows++;
            UpdateValueFreeThrow();
            UpdateValueTotalShootingPercentage();
        }
        private void AddNumberOfMidRanges()
        {
            NumberOfMidRanges++;
            UpdateValueMidRange();
            UpdateValueTotalShootingPercentage();
        }
        private void AddNumberOfThreePointers()
        {
            NumberOfThreePointers++;
            UpdateValueThreePointers();
            UpdateValueTotalShootingPercentage();
        }

        private void AddNumberOfFreeThrowCounted()
        {
            NumberOfFreeThrowsCounted++;
            AddNumberOfFreeThrow();
        }
        private void AddNumberOfMidRangesCounted()
        {
            NumberOfMidRangesCounted++;
            AddNumberOfMidRanges();
        }
        private void AddNumberOfThreePointersCounted()
        {
            NumberOfThreePointersCounted++;
            AddNumberOfThreePointers();
        }

        private void RemoveNumberOfFreeThrow()
        {
            if (NumberOfFreeThrows == NumberOfFreeThrowsCounted &&
                NumberOfFreeThrows > 0)
            {
                NumberOfFreeThrows--;
                NumberOfFreeThrowsCounted--;
                UpdateValueFreeThrow();
                UpdateValueTotalShootingPercentage();
            }
            else if (NumberOfFreeThrows > 0)
            {
                NumberOfFreeThrows--;
                UpdateValueFreeThrow();
                UpdateValueTotalShootingPercentage();
            }
        }
        private void RemoveNumberOfMidRanges()
        {
            if (NumberOfMidRanges == NumberOfMidRangesCounted &&
                NumberOfMidRanges > 0)
            {
                NumberOfMidRanges--;
                NumberOfThreePointersCounted--;
                UpdateValueMidRange();
                UpdateValueTotalShootingPercentage();
            }
            else if (NumberOfMidRanges > 0)
            {
                NumberOfMidRanges--;
                UpdateValueMidRange();
                UpdateValueTotalShootingPercentage();
            }
        }
        private void RemoveNumberOfThreePointers()
        {
            if (NumberOfThreePointers == NumberOfThreePointersCounted &&
                NumberOfThreePointers > 0)
            {
                NumberOfThreePointers--;
                NumberOfThreePointersCounted--;
                UpdateValueThreePointers();
                UpdateValueTotalShootingPercentage();
            }
            else if (NumberOfThreePointers > 0)
            {
                NumberOfThreePointers--;
                UpdateValueThreePointers();
                UpdateValueTotalShootingPercentage();
            }
        }

        private void RemoveNumberOfFreeThrowCounted()
        {
            if (NumberOfFreeThrowsCounted > 0)
            {
                NumberOfFreeThrowsCounted--;
                RemoveNumberOfFreeThrow();
            }
        }
        private void RemoveNumberOfMidRangesCounted()
        {
            if (NumberOfMidRangesCounted > 0)
            {
                NumberOfMidRangesCounted--;
                RemoveNumberOfMidRanges();
            }
        }
        private void RemoveNumberOfThreePointersCounted()
        {
            if (NumberOfThreePointersCounted > 0)
            {
                NumberOfThreePointersCounted--;
                RemoveNumberOfThreePointers();
            }
        }
        
        private void UpdateValueFreeThrow()
        {
            if (null != _currentSession)
            {
                int percentage = 0;

                if (_currentSession.NumberOfFreeThrows > 0)
                {
                    percentage = Convert.ToInt32((_currentSession.NumberOfFreeThrowsCounted / _currentSession.NumberOfFreeThrows) * 100);
                }

                ValueFreeThrow = String.Format(OUT_PERCENTAGE_STRING_TEMPLATE, percentage, _currentSession.NumberOfFreeThrowsCounted, _currentSession.NumberOfFreeThrows);

                if (SpeakEnabled)
                {
                    Speak(String.Format(SPEECH_RATIO_STRING_TEMPLATE, _currentSession.NumberOfFreeThrowsCounted, _currentSession.NumberOfFreeThrows, SPEECH_FREE_THROWS_NAME));
                }
            }
        }
        private void UpdateValueMidRange()
        {
            int percentage = 0;

            if (NumberOfMidRanges > 0)
            {
                percentage = Convert.ToInt32((NumberOfMidRangesCounted / NumberOfMidRanges) * 100);
            }

            ValueMidRange = String.Format(OUT_PERCENTAGE_STRING_TEMPLATE, percentage, NumberOfMidRangesCounted, NumberOfMidRanges);

            if (SpeakEnabled)
            {
                Speak(String.Format(SPEECH_RATIO_STRING_TEMPLATE, NumberOfMidRangesCounted, NumberOfMidRanges, SPEECH_MID_RANGES_NAME));

            }
        }
        private void UpdateValueThreePointers()
        {
            int percentage = 0;

            if (NumberOfThreePointers > 0)
            {
                percentage = Convert.ToInt32((NumberOfThreePointersCounted / NumberOfThreePointers) * 100);
            }

            ValueThreePointers = String.Format(OUT_PERCENTAGE_STRING_TEMPLATE, percentage, NumberOfThreePointersCounted, NumberOfThreePointers);

            if (SpeakEnabled)
            {
                Speak(String.Format(SPEECH_RATIO_STRING_TEMPLATE, NumberOfThreePointersCounted, NumberOfThreePointers, SPEECH_THREE_POINTERS_NAME));
            }
        }
        private void UpdateValueTotalShootingPercentage()
        {
            int percentage = 0;
            double totalShots = _numberOfFreeThrows + NumberOfMidRanges + NumberOfThreePointers;
            double totalShotsCounted = NumberOfFreeThrowsCounted + NumberOfMidRangesCounted + NumberOfThreePointersCounted;

            if (totalShots > 0)
            {
                percentage = Convert.ToInt32((totalShotsCounted / totalShots) * 100);
            }

            ValueTotalShooting = String.Format(OUT_PERCENTAGE_STRING_TEMPLATE, percentage, totalShotsCounted, totalShots);

        }

        private void Speak(string text)
        {
            DependencyService.Get<ITextToSpeech>().Speak(text);
        }

        #endregion

        #region Command Handlers

        private bool CanAddCommand(String obj)
        {
            return true;
        }

        private void OnAddCommand(String obj)
        {
            switch (obj)
            {
                case "CountedFreeThrow":
                    CurrentSession.NumberOfFreeThrows++;
                    CurrentSession.NumberOfFreeThrowsCounted++;
                    UpdateValueFreeThrow();
                    break;
                case "FreeThrow":
                    CurrentSession.NumberOfFreeThrows++;
                    AddNumberOfFreeThrow();
                    break;
                case "CountedMidRange":
                    ButtonIsEnabled = true;
                    AddNumberOfMidRangesCounted();
                    break;
                case "MidRange":
                    AddNumberOfMidRanges();
                    break;
                case "CountedThreePointer":
                    AddNumberOfThreePointersCounted();
                    break;
                case "ThreePointer":
                    AddNumberOfThreePointers();
                    break;
            }

            // Update views
            UpdateValueTotalShootingPercentage();
        }
        
        private bool CanRemoveCommand(string arg)
        {
            return true;
        }

        private void OnRemoveCommand(string obj)
        {
            switch (obj)
            {
                case "CountedFreeThrow":
                    RemoveNumberOfFreeThrowCounted();
                    break;
                case "FreeThrow":
                    RemoveNumberOfFreeThrow();
                    break;
                case "CountedMidRange":
                    RemoveNumberOfMidRangesCounted();
                    break;
                case "MidRange":
                    RemoveNumberOfMidRanges();
                    break;
                case "CountedThreePointer":
                    RemoveNumberOfThreePointersCounted();
                    break;
                case "ThreePointer":
                    RemoveNumberOfThreePointers();
                    break;
            }
        }

        private bool CanListenCommand()
        {
            return true;
        }

        private void OnListenCommand()
        {
            DependencyService.Get<ISpeechToText>().StartListening();
        }

        #endregion

        #region Event Handlers

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            ObservableCollection<ShootingSession> shootingSeshs;
            if (parameters.TryGetValue("ShootingSessions", out shootingSeshs))
            {
                _shootingSessions = shootingSeshs;
            }

            ShootingSession shootingSesh;
            if (parameters.TryGetValue("ShootingSession", out shootingSesh))
            {
                CurrentSession = shootingSesh;
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        #endregion
    }
}
