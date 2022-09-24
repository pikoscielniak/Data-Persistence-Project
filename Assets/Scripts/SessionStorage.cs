using System;
using UnityEngine;

public class SessionStorage : MonoBehaviour
{
    public static SessionStorage Instance { get; private set; }
    public string CurrentUsername { get; private set; }

    public BestScoreData CurrentUserBestScore { get; private set; }
    public BestScoreData GlobalBestScore { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetUsername(string text)
    {
        CurrentUsername = text;
    }

    public bool HasUsername => !string.IsNullOrWhiteSpace(CurrentUsername);

    public string GetBestScoreText()
    {
        var bestScore = Instance.GlobalBestScore;
        if (bestScore != null)
        {
            return $"Best Score: ${bestScore.Username}: ${bestScore.Score}";
        }

        return $"Best Score: Nobody yet";
    }
}

[Serializable]
public class BestScoreData
{
    public string Username { get; set; }
    public int Score { get; set; }
}