using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float health = 100f;
    public float speed = 3f;
    public float attackDamage = 10f;

    private float timer = 0f;

    [SerializeField] protected float attackInterval = 1f;
    [SerializeField] protected float attackRange = 5f;

    private PlayerRPG player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRPG>();
    }

    protected virtual void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            timer += Time.deltaTime;

            if (timer >= attackInterval)
            {
                Attack();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    protected virtual void Attack()
    {
        player.TakeDamage(attackDamage);
    }

    public virtual void Move()
    {
        // Optional movement logic
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
   
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {health}");

        if (health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void ReceiveProjectileHit(float damage)
    {
        Debug.Log($"{gameObject.name} received projectile hit for {damage} damage");
        TakeDamage(damage);
    }
}
