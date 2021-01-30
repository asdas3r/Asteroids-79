using UnityEngine;

public class PlayerEntity : ShootingEntity
{
    public GameObject movers;
    public SpriteRenderer shipRenderer;
    public Color spawnProtectionColor;

    private float spawnProtectionTime = 1f;
    private bool _isSpawnProtectionActive;
    private float _spawnProtectionTimeLeft;
    private string _currentAnimation;
    private Color _defaultShipColor = Color.white;

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
        shipRenderer.color = _defaultShipColor; 
        gameObject.layer = 0;
    }

    public override void Shoot()
    {
        base.Shoot();

        if (_isSpawnProtectionActive)
        {
            DisableSpawnProtection();
        }
    }

    protected override void Death()
    {
        base.Death();

        audioManager.Play("player_death");

        gameManager.PlayerDied();
    }

    public void StartedOnLevel(float protectionTime)
    {
        _isSpawnProtectionActive = true;
        shipRenderer.color = spawnProtectionColor;
        spawnProtectionTime = protectionTime; 
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
