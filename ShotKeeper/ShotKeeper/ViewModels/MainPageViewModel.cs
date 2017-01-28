using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private int _numberOfFreeThrows;
        private int _numberOfMidRanges;
        private int _numberOfThreePointers;

        private int _numberOfFreeThrowsCounted;
        private int _numberOfMidRangesCounted;
        private int _numberOfThreePointersCounted;

        #endregion

        #region Constructors

        public MainPageViewModel()
        {
            _numberOfFreeThrows = 0;
            _numberOfMidRanges = 0;
            _numberOfThreePointers = 0;

            _numberOfFreeThrowsCounted = 0;
            _numberOfMidRangesCounted = 0;
            _numberOfThreePointersCounted = 0;

            UpdateValueFreeThrow();
            UpdateValueMidRange();
            UpdateValueThreePointers();
            UpdateValueTotalShootingPercentage();
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
            _numberOfFreeThrowsCounted++;
            AddNumberOfFreeThrow();
        }

        private void AddNumberOfMidRanges()
        {
            _numberOfMidRanges++;
            UpdateValueMidRange();
            UpdateValueTotalShootingPercentage();
        }

        private void AddNumberOfMidRangesCounted()
        {
            _numberOfMidRangesCounted++;
            AddNumberOfMidRanges();
        }
        private void AddNumberOfThreePointers()
        {
            _numberOfThreePointers++;
            UpdateValueThreePointers();
            UpdateValueTotalShootingPercentage();
        }

        private void AddNumberOfThreePointersCounted()
        {
            _numberOfThreePointersCounted++;
            AddNumberOfThreePointers();
        }

        private void UpdateValueFreeThrow()
        {
            int percentage = 0;

            if (_numberOfThreePointers > 0)
            {
                percentage = _numberOfFreeThrowsCounted / _numberOfFreeThrows;
            }

            ValueFreeThrow = percentage + "% (" + _numberOfFreeThrowsCounted + "/" + _numberOfFreeThrows + ")";
        }

        private void UpdateValueMidRange()
        {
            int percentage = 0;

            if (_numberOfMidRanges > 0)
            {
                percentage = _numberOfMidRangesCounted / _numberOfMidRanges;
            }

            ValueMidRange = percentage + "% (" + _numberOfMidRangesCounted + "/" + _numberOfMidRanges + ")";
        }

        private void UpdateValueThreePointers()
        {
            int percentage = 0;

            if (_numberOfThreePointers > 0)
            {
                percentage = _numberOfThreePointersCounted / _numberOfThreePointers;
            }

            ValueThreePointers = percentage + "% (" + _numberOfThreePointersCounted + "/" + _numberOfThreePointers + ")";
        }

        private void UpdateValueTotalShootingPercentage()
        {
            int percentage = 0;
            int totalShots = _numberOfFreeThrows + _numberOfMidRanges + _numberOfThreePointers;
            int totalShotsCounted = _numberOfFreeThrowsCounted + _numberOfMidRangesCounted + _numberOfThreePointersCounted;

            if (totalShots > 0)
            {
                percentage = totalShotsCounted / totalShots;
            }

            ValueTotalShooting = percentage + "% (" + totalShotsCounted + "/" + totalShots + ")";

        }

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
