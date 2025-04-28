using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;
    public float damage = 20f;

    public AudioClip slimeFailSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit: " + other.gameObject.name);

        // Check if it hit an enemy
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();

        if (enemy != null)
        {
            if (enemy is SlimeEnemy)
            {
                // Hit slime: Play sound, no damage
                if (audioSource != null && slimeFailSound != null)
                {
                    audioSource.PlayOneShot(slimeFailSound);
                }

                Debug.Log("Hit Slime - fail sound played.");
                // Optionally destroy projectile or not
                Destroy(gameObject);
            }
            else
            {
                // Hit other enemy: Deal damage
                enemy.TakeDamage(damage);
                Debug.Log("Hit enemy " + enemy.name + ", dealt " + damage + " damage.");
                Destroy(gameObject);
            }
        }
        else
        {
            // Hit wall / ground
            Destroy(gameObject);
        }
    }
}
