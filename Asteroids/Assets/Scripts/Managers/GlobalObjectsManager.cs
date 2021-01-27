using UnityEngine;

public class GlobalObjectsManager : MonoBehaviour
{
    public static GlobalObjectsManager singleton;

    public GameObject explosionPrefab;
    public GameObject bulletPrefab;

    public GameObject[] asteroidsPrefabs;
    public GameObject[] enemiesPrebafs;
    public GameObject playerPrebaf;

    void Awake()
    {
        if (singleton != null)
        {
            Debug.Log(gameObject.name + " already exists.");
            return;
        }

        singleton = this;
    }
}
