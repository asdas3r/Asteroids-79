using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public float yBorder { get; private set; }
    public float xBorder { get; private set; }
    public int currentLevelNumber { get; private set; }
    public int playerLives { get; private set; }

    private Camera _mainCamera;
    private GlobalObjectsManager _objectsManager;
    private ScoreManager _scoreManager;
    private bool _isLevelStarted;
    private float _pauseCounter;

    private float _respawnTimer;
    private bool _waitingForRespawn;
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
        _scoreManager = ScoreManager.singleton;

        currentLevelNumber = 0;
        _isLevelStarted = false;
        _pauseCounter = 0;
        playerLives = 3;

        UpdateGameBorders();
    }

    void Update()
    {
        UpdateGameBorders();

        if (currentLevelNumber > 0 && _isLevelStarted)
        {
            if (IsLevelCompleted())
            {
                _pauseCounter += Time.deltaTime;

                if (_pauseCounter > 2f)
                {
                    _pauseCounter = 0;
                    StartLevel(++currentLevelNumber);
                }
            }
            else if (_waitingForRespawn)
            {
                _respawnTimer += Time.deltaTime;
                if (_respawnTimer > 2.5f)
                {
                    _respawnTimer = 0;
                    _waitingForRespawn = false;
                    SpawnPlayer();
                }
            }
        }
    }

    private void SpawnPlayer()
    {
        GameObject playerGO = Instantiate(_objectsManager.playerPrebaf);
        _playerEntity = playerGO.GetComponent<PlayerEntity>();
        _playerEntity.StartedOnLevel();
    }

    public void PlayerDied()
    {
        playerLives--;

        if (playerLives == 0)
        {
            EndGame(false);
        }
        else
        {
            _waitingForRespawn = true;
        }
    }

    private void EndGame(bool destroyObjects)
    {
        _isLevelStarted = false;

        if (!destroyObjects)
            return;

        var activeObjects = GetAllActiveGameObjects();
        foreach (var obj in activeObjects)
        {
            Destroy(obj);
        }
    }

    private void UpdateGameBorders()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        xBorder = screenRatio * _mainCamera.orthographicSize;
        yBorder = _mainCamera.orthographicSize;
    }

    public void StartNewGame()
    {
        EndGame(true);

        _scoreManager.ResetPlayerScore();
        currentLevelNumber = 1;
        StartLevel(currentLevelNumber);
        SpawnPlayer();
    }

    public void MenuScreen()
    {
        if (_objectsManager == null)
            _objectsManager = GlobalObjectsManager.singleton;

        StartLevel(0);
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
            int number = Random.Range(0, _objectsManager.asteroidsPrefabs.Length - 1);
            GameObject childAsteroidGO = Instantiate(_objectsManager.asteroidsPrefabs[number]);
            var asteroidEntity = childAsteroidGO.GetComponent<AsteroidEntity>();
            asteroidEntity.levelNumber = levelNumber;
            /*var rules = AsteroidRulesBuilder.GetAsteroidRules(AsteroidForm.None, Vector3.up);
            asteroidEntity.SetupMovement(rules.direction, rules.speed);
            asteroidEntity.asteroidForm = rules.form;
            asteroidEntity.transform.position = new Vector3(Random.Range(-xBorder, xBorder), Random.Range(-yBorder, yBorder), 0);*/
        }

        _isLevelStarted = true;
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
