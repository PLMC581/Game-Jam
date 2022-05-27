using System;
using UnityEngine;

public class SkullPickup : MonoBehaviour
{
    [SerializeField] private float _horizontalThrowForce = 10f;
    [SerializeField] private float _verticalThrowForce = 3f;
    
    private bool _canPickUp;
    private bool _canDamage;

    private PlayerController _player;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_canPickUp)
        {
            if(Input.GetKeyDown(KeyCode.E))
                 _player.PickupSkull(transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            _canPickUp = true;
            _canDamage = false;
        }

        if (_canDamage)
        {
            var _damageable = other.GetComponent<IDamageable>();
            if (_damageable != null)
            {
                _damageable.Hit();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.collider.CompareTag("Ground"))
            _rigidBody.velocity = Vector2.zero;
    }

    public void ThrowSkull(float direction)
    {
        _rigidBody.velocity = new Vector2(_horizontalThrowForce * direction, _verticalThrowForce);
        transform.parent = null;
        _canPickUp = false;
        _rigidBody.isKinematic = false;
        _canDamage = true;
    }
}
