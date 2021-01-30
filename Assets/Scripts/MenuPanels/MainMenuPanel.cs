using UnityEngine;

public class MainMenuPanel : BaseMenuPanel
{
    public GameObject soundOn;
    public GameObject soundOff;

    private GameManager _gameManager;
    private int _soundValue;

    public int SoundValue
    {
        get { return _soundValue; }
        set { _soundValue = value; UpdateButtons(value); }
    }

    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.HasKey("Sound"))
        {
            SoundValue = PlayerPrefs.GetInt("Sound");
        }
        else
        {
            SoundValue = 1;
        }

        _gameManager = GameManager.singleton;
        _gameManager.MenuScreen();
    }

    private void UpdateButtons(int value)
    {
        if (value == 0)
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
            AudioListener.volume = 0f;
        }
        else
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            AudioListener.volume = 1f;
        }

        PlayerPrefs.SetInt("Sound", value);
        PlayerPrefs.Save();
    }

    public void StartNewGame()
    {
        navigationManager.HideCurrentPanel();
        _gameManager.StartNewGame();
    }

    public void ShowControls()
    {
        navigationManager.NavigatePanel("ControlsMenu");
    }

    public void ShowHighscores()
    {
        navigationManager.NavigatePanel("HighscoresMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
