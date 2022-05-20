using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _jumpVelocity = 15f;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpVelocity);
        }
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal") * _speed;

        _rigidBody.velocity = new Vector2(horizontal, _rigidBody.velocity.y);

        
    }
}
