using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace ShotKeeper.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        #region Private Members

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

        #endregion

        #region Commands

        public DelegateCommand<String> AddCommand { get; private set; }

        #endregion

        #region Constructors

        public MainPageViewModel()
        {
            _numberOfFreeThrows = 0;
            NumberOfMidRanges = 0;
            NumberOfThreePointers = 0;

            NumberOfFreeThrowsCounted = 0;
            NumberOfMidRangesCounted = 0;
            NumberOfThreePointersCounted = 0;

            UpdateValueFreeThrow();
            UpdateValueMidRange();
            UpdateValueThreePointers();
            UpdateValueTotalShootingPercentage();

            AddCommand = new DelegateCommand<String>(OnAddCommand, CanAddCommand);
        }

        private bool CanAddCommand(String obj)
        {
            return true;
        }

        private void OnAddCommand(String obj)
        {
            switch(obj)
            {
                case "CountedFreeThrow":
                    AddNumberOfFreeThrowCounted();
                    break;
                case "FreeThrow":
                    AddNumberOfFreeThrow();
                    break;
                case "CountedMidRange":
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
            
        }

        #endregion

        #region Properties

        public string ValueThreePointers
        {
            get { return  _valueThreePointers; }
            set { SetProperty(ref  _valueThreePointers, value); }
        }
        public string ValueMidRange
        {
            get { return _valueMidRange; }
            set { SetProperty(ref _valueMidRange, value); }
        }
        public string ValueFreeThrow
        {
            get { return _valueFreeThrow; }
            set { SetProperty(ref _valueFreeThrow, value); }
        }
        public string ValueTotalShooting
        {
            get { return _valueTotalShooting; }
            set { SetProperty(ref _valueTotalShooting, value); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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

        #endregion

        #region Methods

        private void AddNumberOfFreeThrow()
        {
            _numberOfFreeThrows++;
            UpdateValueFreeThrow();
            UpdateValueTotalShootingPercentage();
        }

        private void AddNumberOfFreeThrowCounted()
        {
            NumberOfFreeThrowsCounted++;
            AddNumberOfFreeThrow();
        }

        private void AddNumberOfMidRanges()
        {
            NumberOfMidRanges++;
            UpdateValueMidRange();
            UpdateValueTotalShootingPercentage();
        }

        private void AddNumberOfMidRangesCounted()
        {
            NumberOfMidRangesCounted++;
            AddNumberOfMidRanges();
        }

        private void AddNumberOfThreePointers()
        {
            NumberOfThreePointers++;
            UpdateValueThreePointers();
            UpdateValueTotalShootingPercentage();
        }

        private void AddNumberOfThreePointersCounted()
        {
            NumberOfThreePointersCounted++;
            AddNumberOfThreePointers();
        }

        private void UpdateValueFreeThrow()
        {
            int percentage = 0;

            if (_numberOfFreeThrows > 0)
            {
                percentage = Convert.ToInt32((NumberOfFreeThrowsCounted / _numberOfFreeThrows) * 100);
            }

            ValueFreeThrow = String.Format("{0}%\n({1}/{2})", percentage, NumberOfFreeThrowsCounted, _numberOfFreeThrows);
        }

        private void UpdateValueMidRange()
        {
            int percentage = 0;

            if (NumberOfMidRanges > 0)
            {
                percentage = Convert.ToInt32((NumberOfMidRangesCounted / NumberOfMidRanges) * 100);
            }

            ValueMidRange = String.Format("{0}%\n({1}/{2})", percentage, NumberOfMidRangesCounted,NumberOfMidRanges);
        }

        private void UpdateValueThreePointers()
        {
            int percentage = 0;

            if (NumberOfThreePointers > 0)
            {
                percentage = Convert.ToInt32((NumberOfThreePointersCounted / NumberOfThreePointers) * 100);
            }

            ValueThreePointers = String.Format("{0}%\n({1}/{2})", percentage, NumberOfThreePointersCounted, NumberOfThreePointers);
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

            ValueTotalShooting = String.Format("{0}%\n({1}/{2})", percentage, totalShotsCounted, totalShots);

        }

        #endregion

        #region Command Handlers

        #endregion

        #region Event Handlers

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }

        #endregion
    }
}
