using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;  
    public float speed;   

    private Rigidbody rigidbody;

    void Start()
    {
       
        rigidbody = GetComponent<Rigidbody>();

        // If the rigidbody is found, set its velocity
        if (rigidbody != null)
        {
            rigidbody.velocity = transform.forward * speed;  
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is an enemy
        BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); 
            enemy.TakeDamage(damage); 
            Destroy(gameObject);  
        }
    }
}
