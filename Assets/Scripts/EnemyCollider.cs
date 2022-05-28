using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponent<PlayerLives>();
        Vector2 normal = other.contacts[0].normal;

        if (player != null)
        {
            if (normal.y <= -0.5f)
            {
                gameObject.SetActive(false);
            }
            else
            {
                player.DecreaseLives();
            }
        }
    }
}
