namespace ShotKeeper.Interfaces
{
    public interface IShootingScore
    {
        double NumberOfFreeThrows { get; set; }
        double NumberOfFieldGoals { get; set; }
        double NumberOfThreePointers { get; set; }

        double NumberOfFreeThrowsCounted { get; set; }
        double NumberOfFieldGoalsCounted { get; set; }
        double NumberOfThreePointersCounted { get; set; }
    }
}
