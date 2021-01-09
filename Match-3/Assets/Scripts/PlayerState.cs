namespace Match3
{
    public class PlayerState
    {
        public readonly float MaxLife;

        public int Score;
        public int CurrentBet;
        public bool Active;
        public float CurrentLife;
        public int StepsCount;
        public int SumOpponentDemage;
        public int SumHealseRestored;
        public int DeltaRatingReward;
        public int DeltaRatingUnreward;

        public PlayerState(float maxLife, int bet)
        {
            MaxLife = maxLife;
            CurrentLife = maxLife;
            CurrentBet = bet;
        }
    }
}
