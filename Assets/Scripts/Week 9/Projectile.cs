using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            Debug.Log($"Hit {enemy.name} for {damage} damage");
            enemy.ReceiveProjectileHit(damage);
        }

        Destroy(gameObject);
    }
}
