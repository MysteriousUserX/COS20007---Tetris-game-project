// File: src/Game/ScoreManager.cs
using System.IO;

public class ScoreManager
{
    private const string HighScoreFilePath = "highscore.txt";
    private int _score;
    private int _highScore;

    public int Score => _score;
    public int HighScore => _highScore;

    public ScoreManager()
    {
        _score = 0;
        _highScore = LoadHighScore();
    }

    public void UpdateScore(int linesCleared)
    {
        switch (linesCleared)
        {
            case 0:
                // No lines cleared, no score change
                break;
            case 1:
                _score += 100;
                break;
            case 2:
                _score += 300;
                break;
            case 3:
                _score += 500;
                break;
            case 4:
                _score += 800;
                break;
            default:
                _score += 800 + (linesCleared - 4) * 300; // Adding 300 points for each additional line cleared beyond 4
                break;
        }

        if (_score > _highScore)
        {
            _highScore = _score;
        }
    }

    private int LoadHighScore()
    {
        if (File.Exists(HighScoreFilePath))
        {
            string scoreText = File.ReadAllText(HighScoreFilePath);
            int.TryParse(scoreText, out int highScore);
            return highScore;
        }
        return 0;
    }

    public void SaveHighScore()
    {
        File.WriteAllText(HighScoreFilePath, _highScore.ToString());
    }

    public void Reset()
    {
        _score = 0;
    }
}
