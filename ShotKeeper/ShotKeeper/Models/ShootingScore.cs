using Prism.Mvvm;
using System;
using ShotKeeper.Interfaces;

namespace ShotKeeper.Models
{
    public class ShootingScore : BindableBase, IShootingScore
    {
        #region Private Members

        private double _numberOfFreeThrows;
        private double _numberOfFieldGoals;
        private double _numberOfThreePointers;

        private double _numberOfFreeThrowsCounted;
        private double _numberOfFieldGoalsCounted;
        private double _numberOfThreePointersCounted;

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
                _numberOfFreeThrows = value;
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
                _numberOfFieldGoals= value;
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
        public double NumberOfFieldGoalsCounted
        {
            get
            {
                return _numberOfFieldGoalsCounted;
            }

            set
            {
                _numberOfFieldGoalsCounted = value;
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
    }
}
