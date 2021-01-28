using UnityEngine;

public class BulletEntity : BaseGameEntity
{
    public float speed = 45f;

    private float timer = 1.5f;

    protected override void OnStart()
    {
        base.OnStart();

        gamePoints = 5;

        SetupMovement(transform.up, speed);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        timer -= Time.deltaTime;
        if (timer <= 0)
            Death();
    }

    protected override void Death()
    {
        Destroy(gameObject);
    }
}
