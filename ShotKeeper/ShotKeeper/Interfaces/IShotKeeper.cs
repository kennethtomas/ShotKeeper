namespace ShotKeeper.Interfaces
{
    public interface IShotKeeper
    {
        #region Properties

        IShootingScore ShootingScore { get; set; }

        #endregion

        #region Methods

        void AddNumberOfFreeThrow();
        void AddNumberOfFieldGoals();
        void AddNumberOfThreePointers();

        void AddNumberOfFreeThrowCounted();
        void AddNumberOfFieldGoalsCounted();
        void AddNumberOfThreePointersCounted();

        void RemoveNumberOfFreeThrow();
        void RemoveNumberOfFieldGoals();
        void RemoveNumberOfThreePointers();

        void RemoveNumberOfFreeThrowCounted();
        void RemoveNumberOfFieldGoalsCounted();
        void RemoveNumberOfThreePointersCounted();

        #endregion
    }
}
