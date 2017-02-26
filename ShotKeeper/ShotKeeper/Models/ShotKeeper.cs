
using System;
using Prism.Mvvm;
using ShotKeeper.Interfaces;

namespace ShotKeeper.Models
{
    public class ShotKeeper : BindableBase, IShotKeeper
    {
        #region Constructors
        public ShotKeeper(IShootingScore shootingScore)
        {
            ShootingScore = shootingScore;
        }
        #endregion

        #region Properties

        public IShootingScore ShootingScore { get; set; }
        
        #endregion

        #region Methods

        public void AddNumberOfFreeThrow()
        {
            ShootingScore.NumberOfFreeThrows++;
        }
        public void AddNumberOfFieldGoals()
        {
            ShootingScore.NumberOfFieldGoals++;
        }
        public void AddNumberOfThreePointers()
        {
            ShootingScore.NumberOfThreePointers++;
        }

        public void AddNumberOfFreeThrowCounted()
        {
            ShootingScore.NumberOfFreeThrowsCounted++;
            AddNumberOfFreeThrow();
        }
        public void AddNumberOfFieldGoalsCounted()
        {
            ShootingScore.NumberOfFieldGoalsCounted++;
            AddNumberOfFieldGoals();
        }
        public void AddNumberOfThreePointersCounted()
        {
            ShootingScore.NumberOfThreePointersCounted++;
            AddNumberOfThreePointers();
        }

        public void RemoveNumberOfFreeThrow()
        {
            if (ShootingScore.NumberOfFreeThrows == ShootingScore.NumberOfFreeThrowsCounted &&
                ShootingScore.NumberOfFreeThrows > 0)
            {
                ShootingScore.NumberOfFreeThrows--;
                ShootingScore.NumberOfFreeThrowsCounted--;
            }
            else if (ShootingScore.NumberOfFreeThrows > 0)
            {
                ShootingScore.NumberOfFreeThrows--;
            }
        }
        public void RemoveNumberOfFieldGoals()
        {
            if (ShootingScore.NumberOfFieldGoals == ShootingScore.NumberOfFieldGoalsCounted &&
                ShootingScore.NumberOfFieldGoals > 0)
            {
                ShootingScore.NumberOfFieldGoals--;
                ShootingScore.NumberOfFieldGoalsCounted--;
            }
            else if (ShootingScore.NumberOfFieldGoals > 0)
            {
                ShootingScore.NumberOfFieldGoals--;
            }
        }
        public void RemoveNumberOfThreePointers()
        {
            if (ShootingScore.NumberOfThreePointers == ShootingScore.NumberOfThreePointersCounted &&
                ShootingScore.NumberOfThreePointers > 0)
            {
                ShootingScore.NumberOfThreePointers--;
                ShootingScore.NumberOfThreePointersCounted--;
            }
            else if (ShootingScore.NumberOfThreePointers > 0)
            {
                ShootingScore.NumberOfThreePointers--;
            }
        }

        public void RemoveNumberOfFreeThrowCounted()
        {
            if (ShootingScore.NumberOfFreeThrowsCounted > 0)
            {
                ShootingScore.NumberOfFreeThrowsCounted--;
                RemoveNumberOfFreeThrow();
            }
        }
        public void RemoveNumberOfFieldGoalsCounted()
        {
            if (ShootingScore.NumberOfFieldGoalsCounted > 0)
            {
                ShootingScore.NumberOfFieldGoalsCounted--;
                RemoveNumberOfFieldGoals();
            }
        }
        public void RemoveNumberOfThreePointersCounted()
        {
            if (ShootingScore.NumberOfThreePointersCounted > 0)
            {
                ShootingScore.NumberOfThreePointersCounted--;
                RemoveNumberOfThreePointers();
            }
        }

        #endregion

    }
}
