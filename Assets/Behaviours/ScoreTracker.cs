public class ScoreTracker
{
    public int Score { get; private set; }

    public void AddPoints(int points)
    {
        if (points > 0)
            Score += points;
    }

    public void Reset()
    {
        Score = 0;
    }
}
