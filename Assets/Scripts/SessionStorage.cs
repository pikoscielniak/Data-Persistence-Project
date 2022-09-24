using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SessionStorage : MonoBehaviour
{
    public static SessionStorage Instance { get; private set; }
    public string CurrentUsername { get; private set; }
    public BestScoreData BestScore { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
    }

    private static string BestScorePath => Path.Combine(Application.persistentDataPath, "best_score.json");

    private void LoadBestScore()
    {
        var path = BestScorePath;
        if (!File.Exists(path)) return;

        var json = File.ReadAllText(path);
        BestScore = JsonConvert.DeserializeObject<BestScoreData>(json);
    }

    public void SetUsername(string text)
    {
        CurrentUsername = text;
    }

    public bool HasUsername => !string.IsNullOrWhiteSpace(CurrentUsername);

    public static string GetBestScoreText()
    {
        var bestScore = Instance.BestScore;
        if (bestScore != null)
        {
            return $"Best Score: {bestScore.Username}: {bestScore.Score}";
        }

        return "Best Score: Nobody yet";
    }

    public void TryUpdateMainScore(int points)
    {
        if (BestScore != null)
        {
            UpdateBestScoreIfIsBeaten(points);
        }
        else
        {
            SetNewBestScore(points);
        }
    }

    private void SetNewBestScore(int points)
    {
        BestScore = new BestScoreData
        {
            Username = CurrentUsername,
            Score = points
        };
        SaveBestScore();
    }

    private void UpdateBestScoreIfIsBeaten(int points)
    {
        if (points <= BestScore.Score) return;
        BestScore.Username = CurrentUsername;
        BestScore.Score = points;
        SaveBestScore();
    }

    private void SaveBestScore()
    {
        if (BestScore == null)
        {
            return;
        }

        var json = JsonConvert.SerializeObject(BestScore);
        File.WriteAllText(BestScorePath, json);
    }
}

public class BestScoreData
{
    public string Username { get; set; }
    public int Score { get; set; }
}