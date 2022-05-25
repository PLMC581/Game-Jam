using System;
using UnityEngine;

public class SkullPickup : MonoBehaviour
{
    private bool _pickedUp;
    
    private PlayerController _player;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_pickedUp)
        {
            if(Input.GetKeyDown(KeyCode.E))
                 _player.PickupSkull(transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player)
            _pickedUp = true;


    }

    public void DropSkull()
    {
        transform.parent = null;
        _pickedUp = false;
        _rigidBody.isKinematic = false;
    }
}
