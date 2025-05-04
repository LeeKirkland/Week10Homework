using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;
    public float damage = 20f;
    public Rigidbody rb;

    public AudioClip slimeFailSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = transform.forward * speed;
        }

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter was triggered."); // Confirm collision
        Debug.Log($"Projectile collided with: {collision.gameObject.name}");

        BaseEnemy enemy = collision.gameObject.GetComponentInParent<BaseEnemy>();
        if (enemy != null)
        {
            if (enemy is SlimeEnemy)
            {
                Debug.Log("Projectile hit a Slime - Playing fail sound, no damage.");
                if (audioSource != null && slimeFailSound != null)
                {
                    audioSource.PlayOneShot(slimeFailSound);
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"Projectile dealing {damage} damage to {enemy.name} via ReceiveProjectileHit()");
                enemy.ReceiveProjectileHit(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("Projectile hit something that is not an enemy.");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter was triggered."); // If using triggers
        Debug.Log($"Projectile trigger hit: {other.gameObject.name}");

        BaseEnemy enemy = other.GetComponentInParent<BaseEnemy>();
        if (enemy != null)
        {
            if (enemy is SlimeEnemy)
            {
                Debug.Log("Trigger hit a Slime - Playing fail sound, no damage.");
                if (audioSource != null && slimeFailSound != null)
                {
                    audioSource.PlayOneShot(slimeFailSound);
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"Trigger dealing {damage} damage to {enemy.name} via ReceiveProjectileHit()");
                enemy.ReceiveProjectileHit(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("Trigger hit something else. Destroying.");
            Destroy(gameObject);
        }
    }
}
