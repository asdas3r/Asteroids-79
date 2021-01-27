using UnityEngine;

public class BulletEntity : BaseGameEntity
{
    private float timer = 1.5f;

    protected override void OnStart()
    {
        base.OnStart();

        gamePoints = 5;

        SetupMovement(transform.up, 45);
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
