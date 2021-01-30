using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoresMenuPanel : BaseMenuPanel
{
    public TMP_Text statusText;
    public Transform scoresPanel;
    public GameObject scoreEntryPrefab;

    private ScoreManager _scoreManager;
    private bool _isScoresAlreadyUpdated;

    protected override void Start()
    {
        base.Start();

        _scoreManager = ScoreManager.singleton;
        _isScoresAlreadyUpdated = false;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (isPanelActive && !_isScoresAlreadyUpdated)
        {
            UpdateScores();
            _isScoresAlreadyUpdated = true;
        }
    }

    private void UpdateScores()
    {
        var childCount = scoresPanel.childCount;

        for (int i = 0; i < childCount; ++i)
        {
            var childTransform = scoresPanel.GetChild(i);
            Destroy(childTransform.gameObject);
        }

        int n = 0;

        foreach (var score in _scoreManager.highscores)
        {
            GameObject scoreEntryGO = Instantiate(scoreEntryPrefab);
            scoreEntryGO.transform.SetParent(scoresPanel);
            scoreEntryGO.transform.localScale = new Vector3(1, 1, 1);
            var hsEntry = scoreEntryGO.GetComponent<HighscoreEntry>();
            hsEntry.SetupEntry(++n, score.Name, score.Score);
        }

        if (n == 0)
        {
            statusText.text = "No highscores yet";
        }
        else
        {
            statusText.text = "";
        }
    }

    public override void NavigateFrom()
    {
        base.NavigateFrom();
        _isScoresAlreadyUpdated = false;
    }

    public void GoBack()
    {
        navigationManager.NavigatePanel("MainMenu");
    }
}
