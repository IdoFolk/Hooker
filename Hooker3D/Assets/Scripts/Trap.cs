using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            var spaceship = collision.gameObject.GetComponent<Spaceship>();
            if (spaceship is not null) spaceship.DeathSFX();
            CheckPoint.SpawnAtLastSpawnPoint(collision.gameObject);
            collision.gameObject.GetComponent<Rigidbody2D>().totalForce = Vector2.zero;
        }
    }
}
