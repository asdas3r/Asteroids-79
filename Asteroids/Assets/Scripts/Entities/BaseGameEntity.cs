using UnityEngine;

public class BaseGameEntity : MonoBehaviour
{
    protected float entityRadius = 1f;
    protected int healthPoints = 1;
    protected int gamePoints = 0;

    public GameObject deathPrefab;

    protected bool isAutoMove { get; private set; } = false;
    public Vector3 movementDirection { get; private set; }
    public float movementSpeed { get; private set; }

    private Collider2D lastCollision;

    protected GlobalObjectsManager objectsManager { get; private set; }
    protected GameManager gameManager { get; private set; }
    protected AudioManager audioManager { get; private set; }

    #region Standart methods

    void Start()
    {
        gameManager = GameManager.singleton;
        objectsManager = GlobalObjectsManager.singleton;
        audioManager = AudioManager.singleton;

        entityRadius = (GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2) * 0.9f;

        OnStart();
    }

    void Update()
    {
        if (isAutoMove)
        {
            SimulateMovement();
        }

        StayInBoundries();

        if (healthPoints <= 0)
        {
            Death();
        }

        OnUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggered(collision);
    }

    #endregion

    #region Private methods

    private void StayInBoundries()
    {
        var pos = transform.position;

        float xMapOffset = gameManager.xBorder + entityRadius;
        float yMapOffset = gameManager.yBorder + entityRadius;

        if (pos.y > yMapOffset)
        {
            pos.y = -yMapOffset;
        }
        else if (pos.y < -yMapOffset)
        {
            pos.y = yMapOffset;
        }
        else if (pos.x > xMapOffset)
        {
            pos.x = -xMapOffset;
        }
        else if (pos.x < -xMapOffset)
        {
            pos.x = xMapOffset;
        }

        transform.position = pos;
    }

    protected Collider2D GetLastDamageCollision()
    {
        return lastCollision;
    }

    #endregion

    #region Inheritance methods

    protected virtual void OnStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void SimulateMovement()
    {
        var pos = transform.position;
        pos += movementDirection.normalized * movementSpeed * Time.deltaTime;
        transform.position = pos;
    }

    protected virtual void Death()
    {
        if (deathPrefab == null)
        {
            deathPrefab = objectsManager.explosionPrefab;
        }

        if (deathPrefab != null)
        {
            Instantiate(deathPrefab, transform.position, transform.rotation);
        }

        if (GetLastDamageCollision().gameObject.CompareTag("Player"))
        {
            gameManager.UpdatePlayerScore(gamePoints);
        }

        Destroy(gameObject);
    }

    protected virtual void OnTriggered(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag) || collision.gameObject.tag == "Bullet")
        {
            return;
        }

        healthPoints--;
        lastCollision = collision;
    }

    #endregion

    public void SetupMovement(Vector3 direction, float speed)
    {
        isAutoMove = true;
        movementDirection = direction;
        movementSpeed = speed;
    }
}