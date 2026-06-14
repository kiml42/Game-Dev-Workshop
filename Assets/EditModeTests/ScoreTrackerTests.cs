using NUnit.Framework;

public class ScoreTrackerTests
{
    [Test]
    public void ScoreStartsAtZero()
    {
        var tracker = new ScoreTracker();
        Assert.AreEqual(0, tracker.Score);
    }

    [Test]
    public void AddPoints_IncreasesScore()
    {
        var tracker = new ScoreTracker();
        tracker.AddPoints(10);
        Assert.AreEqual(10, tracker.Score);
    }

    [Test]
    public void AddPoints_AccumulatesAcrossMultipleCalls()
    {
        var tracker = new ScoreTracker();
        tracker.AddPoints(10);
        tracker.AddPoints(5);
        Assert.AreEqual(15, tracker.Score);
    }

    [Test]
    public void AddPoints_NegativeValue_DoesNotDecreaseScore()
    {
        var tracker = new ScoreTracker();
        tracker.AddPoints(10);
        tracker.AddPoints(-5);
        Assert.AreEqual(10, tracker.Score);
    }

    [Test]
    public void Reset_SetsScoreBackToZero()
    {
        var tracker = new ScoreTracker();
        tracker.AddPoints(42);
        tracker.Reset();
        Assert.AreEqual(0, tracker.Score);
    }
}
