using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class Skeleton : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform _leftSensor;
    [SerializeField] private Transform _rightSensor;
    [FormerlySerializedAs("_speed")] [SerializeField] private float _direction = -1f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        DetermineWhenToTurn();
    }
    private void Move()
    {
        _rigidbody.velocity = new Vector2(_direction, _rigidbody.velocity.y);

        _animator.SetBool("Walking", _direction != 0);
    }

    private void DetermineWhenToTurn()
    {
        ScanSensor(_direction < 0 ? _leftSensor : _rightSensor);
    }

    private void ScanSensor(Transform sensor)
    {
        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result.collider == null)
            TurnAround();
    }

    private void TurnAround()
    {
        _direction *= -1f;
        _spriteRenderer.flipX = _direction > 0;
    }

    public void Hit()
    {
        gameObject.SetActive(false);
    }
}
