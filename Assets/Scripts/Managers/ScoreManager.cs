using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager singleton;

    public List<Highscore> highscores { get; private set; }

    void Awake()
    {
        if (singleton != null)
        {
            Debug.Log(gameObject.name + " already exists.");
            return;
        }

        singleton = this;
    }

    void Start()
    {
        LoadScores();
    }

    public void TryAddScore(Highscore highscore)
    {
        var score = highscore.Score;

        if (IsTopTen(score))
        {
            foreach (var hs in highscores)
            {
                if (hs.Score <= score)
                {
                    hs.Name = highscore.Name;
                    hs.Score = highscore.Score;
                }
            }

            SaveScores();
        }
    }

    private void SaveScores()
    {
        HighscoresHelper.SaveScores(highscores);
    }

    private void LoadScores()
    {
        highscores = HighscoresHelper.LoadHighscores();

        if (highscores == null)
            highscores = HighscoresHelper.LoadDefaultHighscores();
    }

    public bool IsTopTen(float score)
    {
        if (highscores == null)
            return false;

        foreach (var highscore in highscores)
        {
            if (highscore.Score <= score)
                return true;
        }

        return false;
    }
}
