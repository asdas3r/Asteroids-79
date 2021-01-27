using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager singleton;

    public TMPro.TMP_Text gameScoreText;
    public TMPro.TMP_Text highScoreText;
    public long gameScore;

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

    void Update()
    {
        
    }
}
