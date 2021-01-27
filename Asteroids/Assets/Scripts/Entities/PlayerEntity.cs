using System;
using UnityEngine;

public class PlayerEntity : ShootingEntity
{
    public GameObject playerDeathPrefab;
    public GameObject movers;

    private float spawnProtectionTime = 1f;
    private bool _isSpawnProtectionActive;
    private float _spawnProtectionTimeLeft;
    private string _currentAnimation;

    protected override void OnStart()
    {
        base.OnStart();

        _spawnProtectionTimeLeft = spawnProtectionTime;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (_isSpawnProtectionActive)
        {
            _spawnProtectionTimeLeft -= Time.deltaTime; 

            if (_spawnProtectionTimeLeft < 0)
            {
                DisableSpawnProtection();
            }
        }
    }

    private void DisableSpawnProtection()
    {
        _isSpawnProtectionActive = false;
        _spawnProtectionTimeLeft = spawnProtectionTime;
        gameObject.layer = 0;
    }

    protected override void Death()
    {
        if (playerDeathPrefab != null)
        {
            Instantiate(playerDeathPrefab, transform.position, transform.rotation);
        }

        if (GetLastDamageCollision().gameObject.CompareTag("Player"))
        {
            scoresManager.UpdatePlayerScore(-100);
        }

        Destroy(gameObject);

        audioManager.Play("player_death");

        gameManager.PlayerDied();
    }

    public void StartedOnLevel()
    {
        spawnProtectionTime = 1.5f; 
        _isSpawnProtectionActive = true;
        gameObject.layer = 8;
    }

    public void AnimatedMovement(string name)
    {
        var anim = movers.GetComponent<Animator>();
        if (string.Equals(_currentAnimation, name))
        {
            return;
        }

        anim.SetTrigger(name);
        _currentAnimation = name;
    }
}
