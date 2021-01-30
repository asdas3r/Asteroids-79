using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager singleton;

    public int topNumber = 10;
    public TMP_Text gameScoreText;
    public TMP_Text highScoreText;

    public long gameScore { get; private set; }
    public long highScore { get; private set; }

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

        if (highscores.Count > 0)
        {
            highScore = highscores[0].Score;
            highScoreText.text = highScore.ToString();
        }
    }

    public void UpdatePlayerScore(int points)
    {
        gameScore += points;
        gameScoreText.text = gameScore.ToString();
    }

    public void ResetPlayerScore()
    {
        gameScore = 0;
        gameScoreText.text = "00";
    }


    public void TryAddCurrentScore(string name)
    {
        TryAddScore(name, gameScore);
    }

    public void TryAddScore(string name, long score)
    {
        if (IsTopScore(score))
        {
            for (int i = 0; i < highscores.Count; i++)
            {
                var hs = highscores[i];

                if (hs.Score <= score)
                {
                    highscores.Insert(i, new Highscore(name, score));

                    if (highscores.Count > topNumber)
                    {
                        highscores.RemoveRange(topNumber, highscores.Count - topNumber);
                    }

                    SaveScores();
                    return;
                }
            }

            if (highscores.Count < topNumber)
            {
                highscores.Add(new Highscore(name, score));
            }

            SaveScores();
        }
    }

    private void SaveScores()
    {
        highscores = highscores.OrderByDescending(e => e.Score).ToList();
        HighscoresHelper.SaveScores(highscores);
    }

    private void LoadScores()
    {
        highscores = HighscoresHelper.LoadHighscores();

        if (highscores == null)
        {
            highscores = new List<Highscore>();
            //highscores = HighscoresHelper.LoadDefaultHighscores();
        }
    }

    public void CheckForHighscore()
    {
        if (gameScore >= highScore)
        {
            highScore = gameScore;
            highScoreText.text = highScore.ToString();
        }
    }

    public bool IsTopCurrentScore()
    {
        return IsTopScore(gameScore);
    }

    public bool IsTopScore(float score)
    {
        if (highscores == null)
            return false;

        if (highscores.Count < topNumber)
            return true;

        foreach (var highscore in highscores)
        {
            if (highscore.Score <= score)
                return true;
        }

        return false;
    }
}
