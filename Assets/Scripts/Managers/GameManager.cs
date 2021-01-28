using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public float yBorder { get; private set; }
    public float xBorder { get; private set; }
    public int currentLevelNumber { get; private set; }
    public long gameScore { get; private set; }
    public long highScore { get; private set; }
    public int playerLives { get; private set; }

    public Transform livesPanel;
    public GameObject healthPrefab;
    public TMP_Text statusText;
    public TMP_Text gameScoreText;
    public TMP_Text highScoreText;

    private Camera _mainCamera;
    private GlobalObjectsManager _objectsManager;
    private bool _isGameStarted;
    private float _pauseCounter;
    private float _respawnTimer;
    private float _endGameTimer;
    private PlayerStatus _playerStatus;
    private PlayerEntity _playerEntity;

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
        _mainCamera = Camera.main;
        _objectsManager = GlobalObjectsManager.singleton;

        currentLevelNumber = 0;
        _isGameStarted = false;
        _pauseCounter = 0f;
        _respawnTimer = 0f;
        _endGameTimer = 0f;
        playerLives = 3;
        statusText.gameObject.SetActive(false);

        UpdateGameBorders();
    }

    void Update()
    {
        UpdateGameBorders();

        if (_isGameStarted)
        {
            if (IsLevelCompleted())
            {
                _pauseCounter += Time.deltaTime;

                if (_pauseCounter > 2f)
                {
                    _pauseCounter = 0;
                    StartLevel(++currentLevelNumber);
                    if (_playerStatus == PlayerStatus.NotSpawned)
                    {
                        statusText.text = "";
                        SpawnPlayer();
                    }
                }
            }

            if (_playerStatus == PlayerStatus.WaitingForSpawn)
            {
                _respawnTimer += Time.deltaTime;
                if (_respawnTimer > 2.5f)
                {
                    statusText.text = "Press any key to respawn";

                    if (Input.anyKeyDown)
                    {
                        SpawnPlayer();
                        _respawnTimer = 0;
                        statusText.text = "";
                    }
                }
            }
            else if (_playerStatus == PlayerStatus.Dead)
            {
                _endGameTimer += Time.deltaTime;
                if (_endGameTimer > 3f)
                {
                    ShowGameOverScreen();
                }
            }
        }
    }

    private void SpawnPlayer()
    {
        GameObject playerGO = Instantiate(_objectsManager.playerPrebaf);
        _playerEntity = playerGO.GetComponent<PlayerEntity>();
        _playerEntity.StartedOnLevel();
        _playerStatus = PlayerStatus.Alive;
    }

    public void PlayerDied()
    {
        playerLives--;
        UpdateHealthBar();

        if (playerLives == 0)
        {
            _playerStatus = PlayerStatus.Dead;
            statusText.text = "Game over";
        }
        else
        {
            _playerStatus = PlayerStatus.WaitingForSpawn;
        }
    }

    private void EndGame()
    {
        _isGameStarted = false;

        var activeObjects = GetAllActiveGameObjects();
        foreach (var obj in activeObjects)
        {
            Destroy(obj);
        }
    }

    private void ShowGameOverScreen()
    {
        EndGame();
        statusText.text = "";
    }

    private void UpdateGameBorders()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        xBorder = screenRatio * _mainCamera.orthographicSize;
        yBorder = _mainCamera.orthographicSize;
    }

    public void StartNewGame()
    {
        EndGame();

        ResetPlayerScore();
        _isGameStarted = true;
        _playerStatus = PlayerStatus.NotSpawned;
        currentLevelNumber = 0;
        playerLives = 3;
        UpdateHealthBar();
        statusText.gameObject.SetActive(true);
        statusText.text = "Ready player 1";
    }

    public void MenuScreen()
    {
        if (_objectsManager == null)
            _objectsManager = GlobalObjectsManager.singleton;

        StartLevel(0);
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

    private void UpdateHealthBar()
    {
        var childCount = livesPanel.childCount;

        for (int i = 0; i < childCount; ++i)
        {
            var childTransform = livesPanel.GetChild(i);
            Destroy(childTransform.gameObject);
        }

        for (int i = 0; i < playerLives; i++)
        {
            var healthGO = Instantiate(healthPrefab);
            healthGO.transform.SetParent(livesPanel, false);
        }
    }

    public void StartLevel(int levelNumber)
    {
        int asteroidsAmount = 4;

        if (levelNumber > 0)
        {
            asteroidsAmount = 3 + levelNumber;
        }

        for (int i = 0; i < asteroidsAmount; i++)
        {
            int number = UnityEngine.Random.Range(0, _objectsManager.asteroidsPrefabs.Length - 1);
            GameObject childAsteroidGO = Instantiate(_objectsManager.asteroidsPrefabs[number]);
            var asteroidEntity = childAsteroidGO.GetComponent<AsteroidEntity>();
            asteroidEntity.levelNumber = levelNumber;
            /*var rules = AsteroidRulesBuilder.GetAsteroidRules(AsteroidForm.None, Vector3.up);
            asteroidEntity.SetupMovement(rules.direction, rules.speed);
            asteroidEntity.asteroidForm = rules.form;
            asteroidEntity.transform.position = new Vector3(Random.Range(-xBorder, xBorder), Random.Range(-yBorder, yBorder), 0);*/
        }
    }

    private GameObject[] GetAllActiveGameObjects()
    {
        /*var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var player = GameObject.FindGameObjectsWithTag("Player");
        var asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        var bullets = GameObject.FindGameObjectsWithTag("Bullet");*/
        List<GameObject> gameObjects = new List<GameObject>();

        var objects = GameObject.FindObjectsOfType<BaseGameEntity>();
        foreach (var obj in objects)
        {
            gameObjects.Add(obj.gameObject);
        }

        return gameObjects.ToArray();
    }

    private bool IsLevelCompleted()
    {
        List<GameObject> gameObjects = new List<GameObject>();

        var asteroids = GameObject.FindObjectOfType<AsteroidEntity>();
        var enemies = GameObject.FindObjectOfType<EnemyEntity>();

        return (asteroids == null && enemies == null);
    }
}
