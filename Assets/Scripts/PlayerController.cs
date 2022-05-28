using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _jumpVelocity = 15f;
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] private Transform _feet;
    [SerializeField] private float _groundingRadius = .1f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _downpull = 0.1f;
    [SerializeField] private float _maxJumpDuration = 0.1f;

    [SerializeField] private bool _holdingSkull;
    [SerializeField] private Transform _pickupTransform;
    [SerializeField] private float _direction = 1f;

    private Vector3 _startingPos;
    private bool isGrounded;
    private float _fallTimer;
    private float _jumpTimer;

    private int _remainingJumps;

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private GameManager _gameManager;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _remainingJumps = _maxJumps;
        _startingPos = transform.position;
    }

    private void Update()
    {
        if(_gameManager.GameOver) 
            gameObject.SetActive(false);
        
        CheckPlayerGrounding();
        HandleJump();
        HandleSkullPickup();
        if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = -1f;
        }
            
        if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = 1f;
        }
        if(isGrounded)
            _animator.SetBool("Jump", false);
        else
        {
            _animator.SetBool("Jump", true);
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void CheckPlayerGrounding()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, _groundingRadius, _layer);

        isGrounded = hit != null;
    }

    private void HandleSkullPickup()
    {
        if (_holdingSkull)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetComponentInChildren<SkullPickup>().ThrowSkull(_direction);
                _holdingSkull = false;
            }
        }
    }

    private void HandleMovement()
    {
        var horizontal = Input.GetAxis("Horizontal") * _speed;

        _rigidBody.velocity = new Vector2(horizontal, _rigidBody.velocity.y);
        _animator.SetBool("Walk", horizontal != 0);
        if(horizontal != 0)
            _spriteRenderer.flipX = horizontal < 0;

    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           
            if (_remainingJumps > 0)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpVelocity);
                _remainingJumps--;
                _fallTimer = 0;
                _jumpTimer = 0;
            }
        }
        else if (Input.GetButton("Fire1") && _jumpTimer <= _maxJumpDuration)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpVelocity);
            _fallTimer = 0;
        }

        _jumpTimer += Time.deltaTime;

        if (isGrounded && _fallTimer > 0)
        {
            _fallTimer = 0;
            _remainingJumps = _maxJumps;
        }
        else
        {
            _fallTimer += Time.deltaTime;
            var downForce = _downpull * _fallTimer * _fallTimer;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y - downForce);
        }
    }

    public void PickupSkull(Transform skull)
    {
        skull.position = _pickupTransform.position;
        skull.parent = transform;
        skull.GetComponent<Rigidbody2D>().isKinematic = true;
        _holdingSkull = true;
    }

    public void Kill()
    { 
        transform.position = _startingPos;
    }
}
