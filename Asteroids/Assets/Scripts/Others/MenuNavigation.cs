using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    private GameManager _gameManager;

    public CanvasGroup mainMenu;
    public CanvasGroup controls;
    public CanvasGroup highscores;

    private void Start()
    {
        _gameManager = GameManager.singleton;

        _gameManager.MenuScreen();
        ShowMainMenu();
    }

    public void StartNewGame()
    {
        mainMenu.alpha = 0;
        mainMenu.blocksRaycasts = false;

        _gameManager.StartNewGame();
    }

    public void ShowControls()
    {
        mainMenu.alpha = 0;
        mainMenu.blocksRaycasts = false;

        controls.alpha = 1;
        controls.blocksRaycasts = true;
    }

    public void ShowHighscores()
    {
        mainMenu.alpha = 0;
        mainMenu.blocksRaycasts = false;

        highscores.alpha = 1;
        highscores.blocksRaycasts = true;
    }

    public void ShowMainMenu()
    {
        highscores.gameObject.SetActive(true);
        highscores.alpha = 0;
        highscores.blocksRaycasts = false;

        controls.gameObject.SetActive(true);
        controls.alpha = 0;
        controls.blocksRaycasts = false;

        mainMenu.gameObject.SetActive(true);
        mainMenu.alpha = 1;
        mainMenu.blocksRaycasts = true;
    }
}
