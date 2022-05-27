using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxDistance = 2f;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Vector2 _direction = Vector2.up;

    private Vector2 _startingPos;

    private void Start()
    {
        _startingPos = transform.position;
    }

    private void Update()
    {
        transform.Translate(_direction.normalized * Time.deltaTime * _speed);
        var _distance = Vector2.Distance(_startingPos, transform.position);

        if (_distance >= _maxDistance)
        {
            transform.position = _startingPos + (_direction.normalized * _maxDistance);
            _direction *= -1;
        }
    }


    public void Hit()
    {
        gameObject.SetActive(false);
    }
}
