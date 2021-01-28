using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float selfDestructTime;

    private float _destructTimer;

    void Start()
    {
        _destructTimer = selfDestructTime;
    }

    void Update()
    {
        _destructTimer -= Time.deltaTime;

        if (_destructTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
