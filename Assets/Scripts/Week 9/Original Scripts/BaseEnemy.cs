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
    [SerializeField] protected float attackRange = 5f; //modify to make attack range for each enemy

    private PlayerRPG player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRPG>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
      
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);       //checks distance between player and enemy

        if (distanceToPlayer <= attackRange)
        {
       
            timer += Time.deltaTime;        //increases timer if player in range

            if (timer >= attackInterval)
            {
                Attack();
                timer = 0f;
            }
        }
        else
        {
          
            timer = 0f;     //if player is out of range
        }
    }

    protected virtual void Attack()
    {
        player.TakeDamage(attackDamage);
    }

    public virtual void Move()
    {
        // Logic for enemy movement (if necessary)
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
