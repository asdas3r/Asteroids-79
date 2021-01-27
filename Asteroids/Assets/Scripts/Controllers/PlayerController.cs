using UnityEngine;

[RequireComponent(typeof(PlayerEntity))]
public class PlayerController : MonoBehaviour
{
    public float maxVelocity = 30f;
    public float rotateSpeed = 180f;
    public float acceleration = 0.2f;
    public float deceleration = 0.1f;
    public float passiveDeceleration = 0.01f;

    public Vector2 momentVelocity;

    private float _verticalAxis;
    private float _horizontalAxis;
    private Rigidbody2D _rb;
    private PlayerEntity _player;
    private AudioManager _audioManager;

    void Start()
    {
        _player = GetComponent<PlayerEntity>();
        _rb = GetComponent<Rigidbody2D>();
        _audioManager = AudioManager.singleton;

        momentVelocity = Vector3.zero;
    }

    void Update()
    {
        _verticalAxis = Input.GetAxisRaw("Vertical");
        _horizontalAxis = Input.GetAxisRaw("Horizontal");

        if (_verticalAxis != 0)
        {
            float moveSpeed = _verticalAxis > 0 ? acceleration : deceleration;
            Vector2 createdVelocity = transform.up * _verticalAxis * moveSpeed;

            if (_verticalAxis > 0)
            {
                momentVelocity += createdVelocity;

                if (momentVelocity.magnitude > maxVelocity)
                {
                    momentVelocity = momentVelocity.normalized * maxVelocity;
                }

                _player.AnimatedMovement("Forward");
                _audioManager.Play("player_engine", 1.5f);
            }
            /*else
            {
                var momentMagnitudeBefore = momentVelocity.magnitude;
                var tempMomentVelocity = momentVelocity + createdVelocity;
                var momentMagnitudeAfter = tempMomentVelocity.magnitude;

                if (momentMagnitudeAfter < momentMagnitudeBefore)
                {
                    player.AnimatedMovement("Backward");
                    momentVelocity = tempMomentVelocity;
                }
            }*/
        }
        else
        {
            Vector2 passiveDecelerationVelocity = momentVelocity * -1 * passiveDeceleration;
            if (passiveDecelerationVelocity.magnitude > 0)
            {
                momentVelocity += passiveDecelerationVelocity;
            }

            _audioManager.Stop("player_engine");
            _player.AnimatedMovement("Idle");
        }

        _rb.velocity = momentVelocity;

        if (_horizontalAxis != 0)
        {
            transform.Rotate(Vector3.forward, -1 * _horizontalAxis * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _player.Shoot();
            _audioManager.Play("player_shoot");
        }
    }

    private void OnDisable()
    {
        if (_audioManager != null)
            _audioManager.Stop("player_engine");
    }
}
