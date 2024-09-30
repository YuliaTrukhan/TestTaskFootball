namespace _Project.Scripts.Score
{
    public interface IScoreObserver
    {
        void UpdateScore(uint unetId, int score = 1);
    }
}