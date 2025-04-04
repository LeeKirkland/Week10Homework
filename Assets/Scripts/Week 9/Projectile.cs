using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour

{
    public float damage = 10f;  
    public float speed = 10f;  
    public float lifetime = 5f;

    private Rigidbody projectile; 

    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Rigidbody>();  
        projectile.velocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();       //referencing the base enemy script becasue it applies to all enemies
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            return;     //so the projectile doesn't hit the player 
        }
        else
        {
            Destroy(gameObject);
        }
    }

// Update is called once per frame
void Update()
    {
        
    }
}
