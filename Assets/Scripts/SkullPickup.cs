using UnityEngine;
using UnityEngine.Serialization;

public class SkullPickup : MonoBehaviour
{
    [FormerlySerializedAs("_throwForce")] [SerializeField] private float _horizontalThrowForce = 10f;
    [SerializeField] private float _verticalThrowForce = 3f;
    
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

    public void ThrowSkull(float direction)
    {
        _rigidBody.velocity = new Vector2(_horizontalThrowForce * direction, _verticalThrowForce);
        transform.parent = null;
        _pickedUp = false;
        _rigidBody.isKinematic = false;
    }
}
