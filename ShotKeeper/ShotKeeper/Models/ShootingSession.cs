using Prism.Mvvm;
using System;
using ShotKeeper.Interfaces;

namespace ShotKeeper.Models
{
    public class ShootingSession : BindableBase, IShootingScore
    {
        #region Private Members

        private double _numberOfFreeThrows;
        private double _numberOfFieldGoals;
        private double _numberOfThreePointers;

        private double _numberOfFreeThrowsCounted;
        private double _numberOfFieldGoalsCounted;
        private double _numberOfThreePointersCounted;

        private DateTime _createdTime;
        private DateTime _lastModified;

        private int _id;

        #endregion

        #region Properties

        public double NumberOfFreeThrows
        {
            get
            {
                return _numberOfFreeThrows;
            }

            set
            {
                SetProperty(ref _numberOfFreeThrows, value);
            }
        }
        public double NumberOfFieldGoals
        {
            get
            {
                return _numberOfFieldGoals;
            }

            set
            {
                SetProperty(ref _numberOfFieldGoals, value);
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
                SetProperty(ref _numberOfThreePointers, value);
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
                SetProperty(ref _numberOfFreeThrowsCounted, value);
            }
        }
        public double NumberOfFieldGoalsCounted
        {
            get
            {
                return _numberOfFieldGoalsCounted;
            }

            set
            {
                SetProperty(ref _numberOfFieldGoalsCounted, value);

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
                SetProperty(ref _numberOfThreePointersCounted, value);
            }
        }

        public double TotalMisses
        {
            get
            {
                return (NumberOfFieldGoals - NumberOfFieldGoalsCounted) +
                       (NumberOfFreeThrows - NumberOfFreeThrowsCounted) +
                       (NumberOfThreePointers - NumberOfThreePointersCounted);
            }
        }
        public double TotalShots
        {
            get
            {
                return NumberOfFieldGoals + NumberOfFreeThrows + NumberOfThreePointers;
            }
        }
        public double TotalShotsCounted
        {
            get
            {
                return NumberOfFieldGoalsCounted + NumberOfFreeThrowsCounted + NumberOfThreePointersCounted;
            }
        }
        
        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set { SetProperty(ref _createdTime, value); }
        }
        public DateTime LastModified
        {
            get { return _lastModified; }
            set { SetProperty(ref _lastModified, value); }
        }

        public int ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        #endregion
    }
}
