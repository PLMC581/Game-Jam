using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _jumpVelocity = 15f;
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] private Transform _feet;
    [SerializeField] private float _groundingRadius = .1f;
    [SerializeField] private LayerMask _layer;

    private int _currentJumps;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _currentJumps = _maxJumps;
    }

    private void Update()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, _groundingRadius, _layer);
        bool isGrounded = hit != null;
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (_currentJumps > 0)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpVelocity);
            }

            _currentJumps--;
        }

        if (isGrounded)
        {
            _currentJumps = _maxJumps;
        }
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal") * _speed;

        _rigidBody.velocity = new Vector2(horizontal, _rigidBody.velocity.y);

        
    }
}
