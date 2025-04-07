using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRPG : MonoBehaviour
{
    // Power-ups
    public float health = 100f;
    public float attackDamage = 5f;
    public float attackInterval = 1f;
    public float damageReductionFactor = 1f;

    // Projectile stuff
    public int maxAmmo = 10;
    public int currentAmmo;
    public float projectileDamage = 20f;
    public float projectileSpeed = 10f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private float timer;
    private bool isAttackReady = true;

    public Image attackReadyImage;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString("F0");
        }

        if (isAttackReady == false)
        {
            timer += Time.deltaTime;

            if (timer >= attackInterval)
            {
                isAttackReady = true;
                attackReadyImage.gameObject.SetActive(isAttackReady);
                timer = 0f;
            }
        }

        // Actually shooting the projectile; current ammo is limited, reloading resets the ammo amount to max
        if (Input.GetMouseButtonDown(0) && isAttackReady)
        {
            if (currentAmmo > 0)
            {
                ShootProjectile();
                currentAmmo--;
                isAttackReady = false;
                attackReadyImage.gameObject.SetActive(isAttackReady);
            }
        }
    }

    // Getting reference to the Rigidbody and giving it its settings
    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Set the projectile's damage and speed from the PlayerRPG script
        Projectile projScript = projectile.GetComponent<Projectile>();
        projScript.damage = projectileDamage;
        projScript.speed = projectileSpeed;

        // Get Rigidbody component and set velocity to move forward
        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * projectileSpeed;
    }

    // Reloading the amount of ammo
    public void ReloadAmmo()
    {
        currentAmmo = maxAmmo;
        Debug.Log("Ammo reloaded!");
    }

    // Taking damage
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("YOU DIED");
        }
    }
}
