using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            CheckPoint.SpawnAtLastSpawnPoint(collision.gameObject);
            collision.gameObject.GetComponent<Rigidbody2D>().totalForce = Vector2.zero;
        }
    }
}
